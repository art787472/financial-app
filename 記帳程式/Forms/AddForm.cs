using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Components;
using 記帳程式.Forms;
using System.IO;
using 記帳程式.Models;
using CSVLibrary;
using 記帳程式.Services;
using System.Drawing.Imaging;
using 記帳程式.MVP;

namespace 記帳程式.Forms
{
    
    [DisplayName("新增記帳")]
    public partial class AddForm : Form, IAddFormView
    {
        private IAddFormPresenter presenter;

        public AddForm()
        {
            InitializeComponent();
            comboBox1.DataSource = AppData.typeList;
            comboBox1.DisplayMember = "Key";
            ResetImage();

            presenter = DIContainer.GetInstance<IAddFormPresenter>();
            presenter.SetView(this);


            comboBox3.DataSource = new List<string> { "銀行", "Visa", "行動支付" };
           
        }

        private void ResetImage()
        {
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            GC.Collect();
            pictureBox2.Load(@"D:\c_sharp\記帳程式\記帳程式\assets\upload_pic.png");
            pictureBox3.Load(@"D:\c_sharp\記帳程式\記帳程式\assets\upload_pic.png");

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            

            ViewItem viewItem  = new ViewItem();
            viewItem.dateTime = dateTimePicker1.Value;
            viewItem.price = int.Parse(textBox1.Text);
            viewItem.category = comboBox1.Text;
            viewItem.reason = comboBox2.Text;
            viewItem.account = comboBox3.Text;
            viewItem.image1 = pictureBox2.Image;
            viewItem.image2 = pictureBox3.Image;

            presenter.AddRecord(viewItem);

           
            GC.Collect();
            

            
        }

        private void PictureUpload(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images(全部的圖檔)|*.png;*.jpg;*.gif;*.jpeg;*.bmp";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Load(openFileDialog.FileName);
            }
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedValue is KeyValuePair<string,string> item)
            {
                string pickedCategory = item.Value;
                comboBox2.DataSource = AppData.typeDictionary[pickedCategory];
                comboBox2.DisplayMember = "Key";
            }
        }

       
        public void AddRecordFinish(bool isSuccess)
        {
            if(isSuccess)
            {
                MessageBox.Show("新增記帳成功", "訊息");
            }
            textBox1.Text = string.Empty;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            ResetImage();
        }
    }
}
