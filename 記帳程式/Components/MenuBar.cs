using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Forms;
using 記帳程式.Models;

namespace 記帳程式.Components
{
    public partial class MenuBar : UserControl
    {
        
        public MenuBar()
        {
            InitializeComponent();
           
        }
        private void MenuBar_Load(object sender, EventArgs e)
        {
            var theList = Assembly.GetExecutingAssembly().GetTypes().ToList().Where(t => t.Namespace == "記帳程式.Forms").ToList();
            theList = theList.Where(t => t.Name.EndsWith("Form")).ToList();
            var nameList = theList.Select(t => t.GetCustomAttribute<DisplayNameAttribute>().DisplayName).ToList();
            
            var classNameList = theList.Select(t => t.Name).ToList();
            this.Width = 500;
            this.Height = 80;
            this.Controls.Add(GenerateButtonUI(nameList, classNameList));
            SwitchButton((FormCategory)Enum.Parse(typeof(FormCategory), (string)this.Parent.Name, true));
        }

        private void ChangePage(object sender, EventArgs e)
        {
            Button btn = sender as Button;
          
            FormCategory formCategory = (FormCategory)Enum.Parse(typeof(FormCategory), (string)btn.Tag, true);

            SingletonForm.GetForm(formCategory).Show();



            //forms.FirstOrDefault(x => x.Text == btn.Text).Show();


            //this.Parent.Hide();
            //newForm.Show();

            //HW: 要能做到在SingletonForm 控制MenuBar 並且讓MenuBar自己的按鈕Enable = false (這個需要做再MenuBar身上)
            //Tips: 跟 Tag & Enum有關 

            //終極動態結果:只要在Forms資料夾建立新的Form就能自動產生MenuBar Button

        }


        public void SwitchButton(FormCategory formCategory)
        {
            
            foreach(Control control in this.Controls)
            {   
                foreach(Control c in control.Controls)
                {
                    if((string)c.Tag == formCategory.ToString())
                    {
                        c.Enabled = false;
                    }

                }

            }
        }

        private FlowLayoutPanel GenerateButtonUI(List<string> list, List<string> classNameList)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Width = 500;
            flowLayoutPanel.Height = 80;


            foreach (var item in list.Zip(classNameList, (fst, snd) => (fst, snd)))
            {
                Button btn = new Button();
                btn.Width = 70;
                btn.Height = 70;
                btn.Text = item.Item1;
                btn.Tag = item.Item2;
                btn.Click += ChangePage;
                flowLayoutPanel.Controls.Add(btn);
            }
            return flowLayoutPanel;
        }
      
    }
}
