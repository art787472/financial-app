using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 記帳程式.Services
{
    internal class NoteService
    {
        public static List<Item> GetData(DateTime startTime, DateTime endTime)
        {
            List<Item> list = new List<Item>();
            
            var diff = endTime - startTime;
            var days = diff.Days;

            for (int i = 0; i <= days; i++)
            {
                DateTime dateTime = startTime.AddDays(i);
                string directoryName = $@"D:\c_sharp\記帳程式\記帳程式\bin\Debug\{dateTime.ToString("yyyy-MM-dd")}";
                if (Directory.Exists(directoryName))
                {
                    string path = $@"{directoryName}\data.csv";
                    var data = CSVLibrary.CSVHelper.Read<Item>(path);
                    list = list.Concat(data).ToList();
                }
            }
            return list;
        }

        public static void AddImage(DataGridView dataGridView)
        {
            List<Item> list = (List<Item>)dataGridView.DataSource;

            dataGridView.Columns[5].Visible = false;
            dataGridView.Columns[6].Visible = false;
            dataGridView.Columns[7].Visible = false;
            dataGridView.Columns[8].Visible = false;


            DataGridViewImageColumn imageColumn1 = new DataGridViewImageColumn();
            imageColumn1.Name = "ImageColumn";
            imageColumn1.HeaderText = "圖片一";
            imageColumn1.ImageLayout = DataGridViewImageCellLayout.Zoom;

            DataGridViewImageColumn imageColumn2 = new DataGridViewImageColumn();
            imageColumn2.Name = "ImageColumn";
            imageColumn2.HeaderText = "圖片二";
            imageColumn2.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView.Columns.Insert(9, imageColumn1);
            dataGridView.Columns.Insert(10, imageColumn2);

            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {
                Image image = null;
                if(list[row].smallPicPath1 == null)
                {
                    image = Image.FromFile(@"D:\c_sharp\記帳程式\記帳程式\assets\upload_pic.png");
                }
                else
                {

                    image = Image.FromFile(list[row].smallPicPath1);
                    
                }

                ((DataGridViewImageCell)dataGridView.Rows[row].Cells[9]).Value = image;
            }

            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {
                Image image = null;
                if (list[row].smallPicPath2 == null)
                {
                    image = Image.FromFile(@"D:\c_sharp\記帳程式\記帳程式\assets\upload_pic.png");
                }
                else
                {

                    image = Image.FromFile(list[row].smallPicPath2);
                }
                

                ((DataGridViewImageCell)dataGridView.Rows[row].Cells[10]).Value = image;
            }
        }

        public static void AddComboboxColoumn(DataGridView dataGridView)
        {
            DataGridViewComboBoxColumn categoryComboBox = new DataGridViewComboBoxColumn();


            var categories = AppData.typeList.Select(x => x.Key).ToList();
            categoryComboBox.DataSource = categories;

            categoryComboBox.ValueType = typeof(string);

            categoryComboBox.HeaderText = "類別";

            dataGridView.Columns.RemoveAt(2);

            dataGridView.Columns.Insert(2, categoryComboBox);

            DataGridViewComboBoxColumn reasonComboBox = new DataGridViewComboBoxColumn();
            reasonComboBox.ValueType = typeof(string);
            reasonComboBox.HeaderText = "消費目的";
            dataGridView.Columns.RemoveAt(3);
            dataGridView.Columns.Insert(3, reasonComboBox);

            DataGridViewComboBoxColumn accountComboBox = new DataGridViewComboBoxColumn();

            var accounts = new List<string> { "銀行", "Visa", "行動支付" };
            accountComboBox.DataSource = accounts;
            accountComboBox.ValueType = typeof(string);
            accountComboBox.HeaderText = "帳戶";

            dataGridView.Columns.RemoveAt(4);

            dataGridView.Columns.Insert(4, accountComboBox);

            List<Item> list = (List<Item>)dataGridView.DataSource;

            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {
                var cell = ((DataGridViewComboBoxCell)dataGridView.Rows[row].Cells[2]);
                var dataSource = cell.DataSource;
                //cell.Value = "食";
                var data = list[row];
                cell.Value = list[row].category;

                var reasonCell = ((DataGridViewComboBoxCell)dataGridView.Rows[row].Cells[3]);
                var reasons = AppData.typeDictionary[data.category].Select(x => x.Key).ToList();
                reasonCell.DataSource = reasons;
                reasonCell.Value = data.reason;

                var accountCell= ((DataGridViewComboBoxCell)dataGridView.Rows[row].Cells[4]);
                accountCell.Value = data.account;
            }


            

        }
        public static void AddDelete(DataGridView dataGridView)
        {
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "DeleteColumn";
            imageColumn.HeaderText = "Delete";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            dataGridView.Columns.Add(imageColumn);
            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {
                var cell = ((DataGridViewImageCell)dataGridView.Rows[row].Cells[dataGridView.ColumnCount - 1]);
                cell.Value = Image.FromFile(@"D:\c_sharp\記帳程式\記帳程式\assets\trash-can.png");
            }

        }
    }
}
