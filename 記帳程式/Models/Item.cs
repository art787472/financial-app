using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳程式.Models
{
    internal class Item
    {
        [DisplayName("日期")]
        public string dateTime { get; set; }
        [DisplayName("金額")]
        public int price {  get; set; }
        [DisplayName("類別")]
        public string category {  get; set; }
        [DisplayName("消費目的")]
        public string reason {  get; set; }
        [DisplayName("帳戶")]
        public string account {  get; set; }
        [DisplayName("圖片一路徑")]
        public string picPath1 { get; set; }
        [DisplayName("圖片二路徑")]
        public string picPath2 { get; set; }
        [DisplayName("縮圖一路徑")]
        public string smallPicPath1 {  get; set; }
        [DisplayName("縮圖二路徑")]
        public string smallPicPath2 { get; set; }
        
    }
}
