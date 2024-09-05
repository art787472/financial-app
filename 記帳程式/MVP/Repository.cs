using CSVLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳程式.Models;

namespace 記帳程式.MVP
{
    internal class Repository : IRepository
    {


        public void AddRecord(Item data)
        {
            string directoryName = DateTime.Parse(data.dateTime).ToString("yyyy-MM-dd");
            string filePath = ConfigurationManager.AppSettings["filePath"];
            string directoryFullName = Path.Combine(filePath, $@"{directoryName}\data.csv");
            CSVHelper.Write<Item>(data, directoryFullName);
        }

        public void DeleteRecord(Item data)
        {

            
            string directoryName = DateTime.Parse(data.dateTime).ToString("yyyy-MM-dd");


            string filePath = ConfigurationManager.AppSettings["filePath"];
            string path = Path.Combine(filePath, $@"{directoryName}\data.csv");
            List<Item> list = CSVLibrary.CSVHelper.Read<Item>(path);
            list.Remove(data);
            File.Delete(data.smallPicPath1);
            File.Delete(data.smallPicPath2);
            File.Delete(path);
            File.Delete(data.picPath1);
            File.Delete(data.picPath2);

            CSVLibrary.CSVHelper.Write(list, path);
        }

        public void EditRecord(List<Item> list)
        {
            var data = list[0];
            string directoryName = DateTime.Parse(data.dateTime).ToString("yyyy-MM-dd");
            string filePath = ConfigurationManager.AppSettings["filePath"];
            string path = Path.Combine(filePath, $@"{directoryName}\data.csv");
            File.Delete(path);

            CSVLibrary.CSVHelper.Write(list, path);
        }

        public List<Item> GetRecords(DateTime startTime, DateTime endTime)
        {
            List<Item> list = new List<Item>();

            var diff = endTime - startTime;
            var days = diff.Days;

            for (int i = 0; i <= days; i++)
            {
                DateTime dateTime = startTime.AddDays(i);
                
                string filePath = ConfigurationManager.AppSettings["filePath"];
                string directoryName = Path.Combine(filePath, $@"{dateTime.ToString("yyyy-MM-dd")}");
                
                if (Directory.Exists(directoryName))
                {
                    string path = Path.Combine(directoryName, @"data.csv");
                    var data = CSVLibrary.CSVHelper.Read<Item>(path);
                    list = list.Concat(data).ToList();
                }
            }
            return list;
        }
    }
}
