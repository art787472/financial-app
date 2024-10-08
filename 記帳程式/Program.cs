﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Components;
using 記帳程式.Forms;
using 記帳程式.MVP;

namespace 記帳程式
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DIContainer.Register();

            Application.Run(SingletonForm.GetForm(Models.FormCategory.AddForm));
        }
    }
}
