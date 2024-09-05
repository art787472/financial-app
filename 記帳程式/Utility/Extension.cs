using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;
using 記帳程式.Models;
using 記帳程式.Services;
//using System.Timers;
//using Timer = System.Timers.Timer;

namespace 記帳程式.Utility
{
    internal static class Extension
    {
        private static System.Windows.Forms.Timer timer;
        public static void DebounceTime(this Form form, Action callback, double miliseconds)
        {
            
            if(timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
            
            if(timer == null)
            {
                timer = new System.Windows.Forms.Timer();
                timer.Interval = (int)miliseconds;
                //timer.AutoReset = false;
                
                timer.Tick += (Object source, EventArgs e) => { 
                    callback.Invoke();
                    timer.Stop();
                    timer.Dispose();
                    timer = null;
                };
                timer.Start();
            }

            
        }

        private static System.Timers.Timer timer1;
        public static void DebounceTime1(this Form form, Action callback, double miliseconds)
        {

            if (timer1 != null)
            {
                timer1.Stop();
                timer1.Dispose();
                timer1 = null;
            }

            

            if (timer1 == null)
            {
                timer1 = new System.Timers.Timer();
                timer1.Interval = miliseconds;
                timer1.AutoReset = false;
                timer1.SynchronizingObject = form;

                timer1.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => 
                {
                    
                    callback.Invoke();
                    timer1.Dispose();
                    timer1 = null;
                };
                   
                timer1.Start();
            }


        }

        private static System.Threading.Timer timer2;

        public static void DebounceTime2(this Form form, Action callback, double miliseconds)
        {
            
            if(timer2 != null)
            {
                timer2.Change((int)miliseconds, Timeout.Infinite);
                return;

            }
            
           
            timer2 = new System.Threading.Timer(Callback, (form, callback), (int)miliseconds, Timeout.Infinite);
            
            
        }

        private static void Callback(Object state)
        {
            (Form, Action) tuple = ((Form, Action))state;
            tuple.Item1.Invoke(new Action(() =>
            {
                tuple.Item2.Invoke();

            }));
        }

        public static void AddCustomColoums(this DataGridView dataGridView)
        {
            dataGridView.AddImage();
            dataGridView.AddComboboxColoumn();
            dataGridView.AddDelete();
        }

        public static void AddImage(this DataGridView dataGridView)
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

            string assetsPath = ConfigurationManager.AppSettings["assetsPath"];
            var defaultImagePath = Path.Combine(assetsPath, @"upload_pic.png");

            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {
                Image image = list[row].smallPicPath1 == null ? Image.FromFile(defaultImagePath) : Image.FromFile(list[row].smallPicPath1);
                Image image2 = list[row].smallPicPath2 == null ? Image.FromFile(defaultImagePath) : Image.FromFile(list[row].smallPicPath2);

                ((DataGridViewImageCell)dataGridView.Rows[row].Cells[9]).Value = image;
                ((DataGridViewImageCell)dataGridView.Rows[row].Cells[10]).Value = image2;

            }
        }

        public static void AddComboboxColoumn(this DataGridView dataGridView)
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

                var accountCell = ((DataGridViewComboBoxCell)dataGridView.Rows[row].Cells[4]);
                accountCell.Value = data.account;
            }




        }
        public static void AddDelete(this DataGridView dataGridView)
        {
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "DeleteColumn";
            imageColumn.HeaderText = "Delete";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            string assetsPath = ConfigurationManager.AppSettings["assetsPath"];
            var trashCanImagePath = Path.Combine(assetsPath, @"trash-can.png");

            dataGridView.Columns.Add(imageColumn);
            for (int row = 0; row < dataGridView.Rows.Count; row++)
            {
                var cell = ((DataGridViewImageCell)dataGridView.Rows[row].Cells[dataGridView.ColumnCount - 1]);
                cell.Value = Image.FromFile(trashCanImagePath);
            }

        }
    }
}
