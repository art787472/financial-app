using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 記帳程式
{
    public partial class PictureForm : Form
    {
        public PictureForm(string picturePath)
        {
            InitializeComponent();
            pictureBox1.Load(picturePath);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void PictureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pictureBox1.Image = null;
            GC.Collect();
        }
    }
}
