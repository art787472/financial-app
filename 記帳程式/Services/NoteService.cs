using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Models;

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
            DataGridViewComboBoxColumn reasonComboBoxColumn = new DataGridViewComboBoxColumn();


            var categories = AppData.typeList.Select(x => x.Key).ToList();
            reasonComboBoxColumn.DataSource = categories;

            reasonComboBoxColumn.ValueType = typeof(string);

            

            dataGridView.Columns[2].Visible = false;

            dataGridView.Columns.Insert(2, reasonComboBoxColumn);

            List<Item> list = (List<Item>)dataGridView.DataSource;

            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {

                ((DataGridViewComboBoxCell)dataGridView.Rows[row].Cells[2]).Value = list[row].category;
            }



        }
    }
}
