using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳程式.Models
{
    internal class ViewItem
    {
        [DisplayName("日期")]
        public DateTime dateTime { get; set; }
        [DisplayName("金額")]
        public int price { get; set; }
        [DisplayName("類別")]
        public string category { get; set; }
        [DisplayName("消費目的")]
        public string reason { get; set; }
        [DisplayName("帳戶")]
        public string account { get; set; }

        public Image image1 { get; set; }
        public Image image2 { get; set; }
        
    }
}
