using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CheckBox = System.Windows.Forms.CheckBox;

namespace 記帳程式.Components
{
    public partial class FilterCheckbox : UserControl
    {
        private List<string> filters = new List<string>();
        private List<string> check = new List<string>();
        public List<string> Filters 
        {
            get => check; 
        }

        public List<string> AllFilters
        {
            get => filters;
        }

        public FilterCheckbox()
        {
            InitializeComponent();
            this.Width = 400;
        }

        private void FilterCheckbox_Load(object sender, EventArgs e)
        {
          
            flowLayoutPanel1.Width = 400;
            flowLayoutPanel1.Height = this.Height;
        }

        public void SetFilters(List<string> filters)
        {
            this.filters = filters;
            this.check = new List<string>();
            this.flowLayoutPanel1.Controls.Clear();
            foreach (string filter in filters)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Width = 60;
                checkBox.BackColor = Color.AliceBlue;
                checkBox.Text = filter;
                checkBox.CheckedChanged += Checked;
                flowLayoutPanel1.Controls.Add(checkBox);
            }
        }

        private void Checked(object sender, EventArgs e) 
        {
            CheckBox checkBox = (CheckBox) sender;
            
            if(checkBox.Checked)
            {
                check.Add(checkBox.Text);
            }
            else
            {
                
                check.Remove(check.FirstOrDefault(x => x == checkBox.Text));
            }
            if(onCheckChange != null)
            {
                onCheckChange.Invoke(check);
            }

        }

        public delegate void OnCheckChange(List<string> check);
        public OnCheckChange onCheckChange;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                check.Clear();
                foreach (var item in filters)
                {
                    check.Add(item);
                }
                foreach (var item in flowLayoutPanel1.Controls)
                {
                    if (item is CheckBox)
                    {
                        CheckBox box = item as CheckBox;
                        if (box.Text != "全部")
                        {
                            box.Checked = true;

                        }
                    }
                }
                return;

            }
            foreach (var item in flowLayoutPanel1.Controls)
            {
                check.Clear();
                if (item is CheckBox)
                {
                    CheckBox box = item as CheckBox;
                    if (box.Text != "全部")
                    {
                        box.Checked = false;
                        
                    }
                }

            }

            if (onCheckChange != null)
            {
                onCheckChange.Invoke(check);
            }
        }
    }
}
