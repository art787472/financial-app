using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Models;
using 記帳程式.Services;
using 記帳程式.Utility;

namespace 記帳程式.Forms
{
    [DisplayName("記帳本")]
    public partial class NoteForm : Form
    {

        private List<Item> list;
        public NoteForm()
        {
            InitializeComponent();
            
        }

        private void NoteForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var monthDays = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1) - new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, monthDays.Days);

            list = NoteService.GetData(dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.DataSource = list;

            NoteService.AddImage(dataGridView1);
            NoteService.AddComboboxColoumn(dataGridView1);
            NoteService.AddDelete(dataGridView1);

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIdx = e.ColumnIndex;
            int rowIdx = e.RowIndex;

            if(colIdx >= 7 && colIdx <= 8)
            {
                string picturePath = (string)dataGridView1.Rows[rowIdx].Cells[colIdx - 4].Value;
                PictureForm picForm = new PictureForm(picturePath);
                picForm.ShowDialog();
            }

            if (dataGridView1.Columns[colIdx].Name == "DeleteColumn")
            {
                

                // Edit data
                Item data = list[rowIdx];
                
                list.RemoveAt(rowIdx);
                

                // Reload UI
                DataGridViewReload();

                // Edit CSV
                string directoryName = DateTime.Parse(data.dateTime).ToString("yyyy-MM-dd");

                List<Item> newlist = list.Where(x => DateTime.Parse(x.dateTime).ToString("yyyy-MM-dd") == directoryName).ToList();

                string path = $@"D:\c_sharp\記帳程式\記帳程式\bin\Debug\{directoryName}\data.csv";
                File.Delete(path);
                File.Delete(data.picPath1);
                File.Delete(data.picPath2);
                File.Delete(data.smallPicPath1);
                File.Delete(data.smallPicPath2);

                CSVLibrary.CSVHelper.Write(newlist, path);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DebounceTime2(() =>
            {
                Console.WriteLine("click on search!");
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = null;
                GC.Collect();
                list = NoteService.GetData(dateTimePicker1.Value, dateTimePicker2.Value);

                dataGridView1.DataSource = list;

                NoteService.AddImage(dataGridView1);
                NoteService.AddComboboxColoumn(dataGridView1);
                NoteService.AddDelete(dataGridView1);
            }, 500);
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            DataGridView dataGridView = (DataGridView)sender;
             
            string editedData = (string)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
            
            if(oldValue == editedData)
            {
                return;
            }

            //List<Item> list = (List<Item>)dataGridView.DataSource;

            Item data = list[e.RowIndex];

            var propertyInfo = typeof(Item).GetProperties()[e.ColumnIndex];
            propertyInfo.SetValue(data, Convert.ChangeType(editedData, propertyInfo.PropertyType));

            if(e.ColumnIndex == 2) // 處理變更類別連帶影響消費理由
            {
                var reasonCell = (DataGridViewComboBoxCell)dataGridView.Rows[e.RowIndex].Cells[3];
                var reasons = AppData.typeDictionary[data.category].Select(x => x.Key).ToList();
                reasonCell.DataSource = reasons;
                var defaultValue = reasons[0];
                reasonCell.Value = defaultValue;
                
                var reasonInfo = typeof(Item).GetProperties()[3];
                reasonInfo.SetValue(data, defaultValue);
            }

            string directoryName = DateTime.Parse(data.dateTime).ToString("yyyy-MM-dd");

            List<Item> newlist = list.Where(x => DateTime.Parse(x.dateTime).ToString("yyyy-MM-dd") == directoryName).ToList();

            string path = $@"D:\c_sharp\記帳程式\記帳程式\bin\Debug\{directoryName}\data.csv";
            File.Delete(path);

            CSVLibrary.CSVHelper.Write(newlist, path);
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                if ((string)combo.Items[0] == "食")
                {
                    
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private string oldValue = "";
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            oldValue = (string)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
        }

        private void DataGridViewReload()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            GC.Collect();

            dataGridView1.DataSource = list;

            NoteService.AddImage(dataGridView1);
            NoteService.AddComboboxColoumn(dataGridView1);
            NoteService.AddDelete(dataGridView1);
        }
    }
}
