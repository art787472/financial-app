using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Components;
using 記帳程式.Models;

namespace 記帳程式
{
    internal class SingletonForm
    {
        private static Form currentForm;
        private static Dictionary<FormCategory, Form> formDict = new Dictionary<FormCategory, Form>();

        public static Form GetForm(FormCategory formCategory)
        {
            string typeName = "記帳程式.Forms." + formCategory.ToString();
            Type t = Type.GetType(typeName);
            if (SingletonForm.currentForm != null)
            {
                SingletonForm.currentForm.Hide();

            }
            if (!formDict.ContainsKey(formCategory)) 
            {
                formDict[formCategory] = (Form)Activator.CreateInstance(t);
            }
            
            currentForm = formDict[formCategory];

            var menuBar =  currentForm.GetType().GetFields().Where(x => x.FieldType.Name == "MenuBar").FirstOrDefault();

            MenuBar menu = (MenuBar)menuBar.GetValue(currentForm);

            menu.SwitchButton(formCategory);
            return currentForm;
        }
    }
}
