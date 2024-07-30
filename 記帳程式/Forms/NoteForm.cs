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
        
        public NoteForm()
        {
            InitializeComponent();
            
        }

        private void NoteForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 31);

            List<Item> list = NoteService.GetData(dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.DataSource = list;

            NoteService.AddImage(dataGridView1);
            NoteService.AddComboboxColoumn(dataGridView1);

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIdx = e.ColumnIndex;
            int rowIdx = e.RowIndex;

            if(colIdx >= 7)
            {
                string picturePath = (string)dataGridView1.Rows[rowIdx].Cells[colIdx - 4].Value;
                PictureForm picForm = new PictureForm(picturePath);
                picForm.ShowDialog();
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
                List<Item> list = NoteService.GetData(dateTimePicker1.Value, dateTimePicker2.Value);

                dataGridView1.DataSource = list;

                NoteService.AddImage(dataGridView1);
                NoteService.AddComboboxColoumn(dataGridView1);

            }, 500);
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            string editedData = (string)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            List<Item> list = (List<Item>)dataGridView.DataSource;

            Item data = list[e.RowIndex];

            var propertyInfo = typeof(Item).GetProperties()[e.ColumnIndex];
            propertyInfo.SetValue(data, Convert.ChangeType(editedData, propertyInfo.PropertyType));

            string directoryName = DateTime.Parse(data.dateTime).ToString("yyyy-MM-dd");

            list = list.Where(x => DateTime.Parse(x.dateTime).ToString("yyyy-MM-dd") == directoryName).ToList();

            string path = $@"D:\c_sharp\記帳程式\記帳程式\bin\Debug\{directoryName}\data.csv";
            File.Delete(path);

            CSVLibrary.CSVHelper.Write(list, path);
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                
            }
        }
    }
}
