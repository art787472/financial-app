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
using 記帳程式.MVP;
using 記帳程式.Services;
using 記帳程式.Utility;

namespace 記帳程式.Forms
{
    [DisplayName("記帳本")]
    public partial class NoteForm : Form, INoteFormView
    {
        private INoteFormPresenter presenter;
        
        public NoteForm()
        {
            InitializeComponent();
            this.presenter = DIContainer.GetInstance<INoteFormPresenter>();
            this.presenter.SetView(this);
        }

        private void NoteForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var monthDays = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1) - new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, monthDays.Days);

            presenter.LoadRecords(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIdx = e.ColumnIndex;
            int rowIdx = e.RowIndex;

            // 圖片彈窗
            if (dataGridView1.Columns[colIdx].Name == "ImageColumn")
            {
                string picturePath = (string)dataGridView1.Rows[rowIdx].Cells[colIdx - 4].Value;
                PictureForm picForm = new PictureForm(picturePath);
                picForm.ShowDialog();
            }

            // 刪除功能
            if (dataGridView1.Columns[colIdx].Name == "DeleteColumn")
            {
                presenter.DeleteRecord(rowIdx);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DebounceTime2(() =>
            {
                
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = null;
                GC.Collect();

                presenter.LoadRecords(dateTimePicker1.Value, dateTimePicker2.Value);

                
            }, 500);
           
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            DataGridView dataGridView = (DataGridView)sender;
             
            string editedData = (string)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
            
            if(oldValue == editedData)
            {
                return;
            }
            

            if(e.ColumnIndex == 2) // 處理變更類別連帶影響消費理由
            {
                var category = (string)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                var reasonCell = (DataGridViewComboBoxCell)dataGridView.Rows[e.RowIndex].Cells[3];
                var reasons = AppData.typeDictionary[category].Select(x => x.Key).ToList();
                reasonCell.DataSource = reasons;
                var defaultValue = reasons[0];
                reasonCell.Value = defaultValue;
                
            }
            presenter.EditRecord(e.RowIndex, e.ColumnIndex, editedData);
            
        }

        private string oldValue = "";
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            oldValue = (string)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
        }

        private void DataGridViewReload(List<Item> items)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            GC.Collect();

            dataGridView1.DataSource = items;

            dataGridView1.AddCustomColoums();
        }

        public void LoadRecords(List<Item> items)
        {
            
            dataGridView1.DataSource = items;
            dataGridView1.AddCustomColoums();
        }

        public void DeleteRecordFinish(bool isSucess, List<Item> items)
        {
            if(!isSucess)
            {
                MessageBox.Show("刪除失敗");
                return;
            }

            DataGridViewReload(items);
            MessageBox.Show("刪除成功");

        }

        public void EditRecordFinish(bool isSucess, List<Item> items)
        {
            if (!isSucess)
            {
                MessageBox.Show("編輯失敗");
                return;
            }

            MessageBox.Show("編輯成功");
        }
    }
}
