using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳程式.Models;
using 記帳程式.Services;

namespace 記帳程式.MVP
{
    internal class SearchFormPresenter : ISearchFormPresenter
    {
        private ISearchFormView view;
        private IRepository repository;
        private List<Item> list = new List<Item>();
        public void SearchRecords(DateTime begin, DateTime end, List<string> categories, List<string> reasons, List<string> accounts)
        {
            var result1 = RecordService.GetRecords(begin, end, categories, reasons, accounts);
            view.SearchRecordsFinish(result1);
        }

        public void SetView(ISearchFormView form)
        {
            this.view = form;
            this.repository = DIContainer.GetInstance<IRepository>();
        }

        private List<Item> filterList(string condition, string propertyName ,List<Item> list)
        {
            if (condition != "全部")
            {
                var properties = typeof(Item).GetProperties();
                var property = properties.FirstOrDefault(x => x.Name == propertyName);
                list = list.Where(r => (string)property.GetValue(r) == condition).ToList();
                return list;
            }
            return list;
        }
    }
}
