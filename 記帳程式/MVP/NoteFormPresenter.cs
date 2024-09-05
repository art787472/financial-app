using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Models;

namespace 記帳程式.MVP
{
    internal class NoteFormPresenter : INoteFormPresenter
    {
        private INoteFormView view;
        private IRepository repository;
        private List<Item> list;

        
        public void DeleteRecord(int idx)
        {
            
            Item data = list[idx];
            list.RemoveAt(idx);
            
            view.DeleteRecordFinish(true, list);
            string directoryName = DateTime.Parse(data.dateTime).ToString("yyyy-MM-dd");
            var newList = list.Where(x => DateTime.Parse(x.dateTime).ToString("yyyy-MM-dd") == directoryName).ToList();

            repository.DeleteRecord(data);

        }

        public void EditRecord(int rowIdx, int colIdx, string editedData)
        {
            Item data = list[rowIdx];
            var propertyInfo = typeof(Item).GetProperties()[colIdx];
            propertyInfo.SetValue(data, Convert.ChangeType(editedData, propertyInfo.PropertyType));

            if (colIdx == 2) // 處理變更類別連帶影響消費理由
            {
                
                var reasons = AppData.typeDictionary[data.category].Select(x => x.Key).ToList();
                var defaultValue = reasons[0];

                var reasonInfo = typeof(Item).GetProperties()[3];
                reasonInfo.SetValue(data, defaultValue);
            }

            string directoryName = DateTime.Parse(data.dateTime).ToString("yyyy-MM-dd");
            var newlist = list.Where(x => DateTime.Parse(x.dateTime).ToString("yyyy-MM-dd") == directoryName).ToList();



            repository.EditRecord(newlist);
            view.EditRecordFinish(true, list);


        }

        public void LoadRecords(DateTime startTime, DateTime endTime)
        {
            List<Item> list = repository.GetRecords(startTime, endTime);
            this.list = list;
            view.LoadRecords(list);
        }

        public void SetView(INoteFormView view)
        {
            this.view = view;
            this.repository = DIContainer.GetInstance<IRepository>();
        }
    }
}
