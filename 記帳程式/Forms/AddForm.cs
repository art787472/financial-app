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

namespace 記帳程式.Forms
{
    [DisplayName("新增記帳")]
    public partial class AddForm : Form
    {
        

        public AddForm()
        {
            InitializeComponent();
            comboBox1.DataSource = AppData.typeList;
            comboBox1.DisplayMember = "Key";
            ResetImage();


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

        private void button1_Click(object sender, EventArgs e)
        {
            AccountForm form = new AccountForm();
            form.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string directoryName = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string directoryFullName = $@"D:\c_sharp\記帳程式\記帳程式\bin\Debug\{directoryName}\data.csv";

            string fileName1 = Guid.NewGuid().ToString();
            string fileName2 = Guid.NewGuid().ToString();

            string imagePath1 = $@"D:\c_sharp\記帳程式\記帳程式\bin\Debug\{directoryName}\{fileName1}.jpeg";
            string imagePath2 = $@"D:\c_sharp\記帳程式\記帳程式\bin\Debug\{directoryName}\{fileName2}.jpeg";

            string smallImgPath1 = $@"D:\c_sharp\記帳程式\記帳程式\bin\Debug\{directoryName}\{fileName1}-small.jpeg";
            string smallImgPath2 = $@"D:\c_sharp\記帳程式\記帳程式\bin\Debug\{directoryName}\{fileName2}-small.jpeg";

            Item newItem = new Item();
            newItem.dateTime = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
            newItem.price = int.Parse(textBox1.Text);
            newItem.category = comboBox1.Text;
            newItem.reason = comboBox2.Text;
            newItem.account = comboBox3.Text;
            newItem.picPath1 = imagePath1;
            newItem.picPath2 = imagePath2;
            newItem.smallPicPath1 = smallImgPath1;
            newItem.smallPicPath2 = smallImgPath2;

            // D:\c_sharp\記帳程式\記帳程式\bin\Debug\2024-07-18

            CSVHelper.Write<Item>(newItem, directoryFullName);

            
            Bitmap bitmap1 = CompressImage.Compress(pictureBox2.Image, 25, CompressCategory.NormalCompress);
            bitmap1.Save(imagePath1,ImageFormat.Jpeg);
            bitmap1.Dispose();
            Bitmap bitmap2 = CompressImage.Compress(pictureBox3.Image, 25, CompressCategory.NormalCompress);
            bitmap2.Save(imagePath2, ImageFormat.Jpeg);
            bitmap2.Dispose();

            Bitmap smallBitmap1 = CompressImage.Compress(pictureBox2.Image, 1, CompressCategory.SizeCompress);
            smallBitmap1.Save(smallImgPath1, ImageFormat.Jpeg);
            smallBitmap1.Dispose();

            Bitmap smallBitmap2 = CompressImage.Compress(pictureBox3.Image, 1, CompressCategory.SizeCompress);
            smallBitmap2.Save(smallImgPath2, ImageFormat.Jpeg);
            smallBitmap2.Dispose();

            ResetImage();
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

        private void button2_Click(object sender, EventArgs e)
        {
            using (Student student = new Student(1, "leo"))
            {
                student.ShowInfo();
            }
              
        }
    }
}
