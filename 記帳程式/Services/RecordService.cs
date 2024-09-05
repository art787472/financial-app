using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using 記帳程式.Models;
using 記帳程式.MVP;

namespace 記帳程式.Services
{
    internal class RecordService
    {
        private static IRepository repository = DIContainer.GetInstance<IRepository>();

        public static List<Item> GetRecords(DateTime begin, DateTime end, List<string> categories, List<string> reasons, List<string> accounts)
        {
            List<Item> records = repository.GetRecords(begin, end);
            var result = records
                .Where(x => (categories.Count == 0 || categories.Contains(x.category)) &&
                            (reasons.Count == 0    || reasons.Contains(x.reason))      &&
                            (accounts.Count == 0   || accounts.Contains(x.account)))
                .GroupBy(x => new
                {
                    Category = categories != null && categories.Any() && categories.Contains(x.category) ? x.category : null,
                    Reason = reasons != null      && reasons.Any()    && reasons.Contains(x.reason)      ? x.reason   : null,
                    Account = accounts != null    && accounts.Any()   && accounts.Contains(x.account)    ? x.account  : null
                })
                .Select(x => new Item()
                {
                    category = x.Key.Category,
                    reason = x.Key.Reason,
                    account = x.Key.Account,
                    price = x.Sum(y => y.price)
                }).ToList();
            return result;
        }

        public static (string[], Double[]) GetRecordsForChart(DateTime begin, DateTime end)
        {
            var data = repository.GetRecords(begin, end);

            var dataX = data.GroupBy(x => x.category).Select(x => x.Key).ToArray<string>();

            var dataY = data.GroupBy(x => x.category).Select(x => x.Sum(y => y.price)).Select(Convert.ToDouble).ToArray();

            return (dataX, dataY);
        }

        public static (string[], Double[]) GetRecordsForLineChart(DateTime begin, DateTime end)
        {
            var data = repository.GetRecords(begin, end);
            var dataGroupBy = data
                .GroupBy(x =>  new {
                    Date = DateTime.Parse(x.dateTime).Date.ToString("yyyy-MM-dd")
                    
                }).Select(x => new Item
                {
                   dateTime = x.Key.Date,
                   price = x.Sum(y => y.price)
                })
                .ToList();

            var dataX = dataGroupBy.Select(x => x.dateTime).ToArray();
            var dataY = dataGroupBy.Select(x => x.price).Select(Convert.ToDouble).ToArray();
            return (dataX, dataY);
        }

        public static (string[], Double[]) GetRecordsForLineChart(DateTime begin, DateTime end, List<string> categories, List<string> reasons, List<string> accounts)
        {
            

            List<Item> records = repository.GetRecords(begin, end);
            var result = records
                .Where(x => (categories.Count == 0 || categories.Contains(x.category)) &&
                            (reasons.Count == 0    || reasons.Contains(x.reason))      &&
                            (accounts.Count == 0   || accounts.Contains(x.account)))
                .GroupBy(x => new
                {
                    Category = categories != null && categories.Any() && categories.Contains(x.category) ? x.category : null,
                    Reason = reasons != null && reasons.Any() && reasons.Contains(x.reason) ? x.reason : null,
                    Account = accounts != null && accounts.Any() && accounts.Contains(x.account) ? x.account : null,
                    Date = DateTime.Parse(x.dateTime).Date.ToString("yyyy-MM-dd")
                })
                .Select(x => new Item()
                {
                    dateTime = x.Key.Date,
                    category = x.Key.Category,
                    reason = x.Key.Reason,
                    account = x.Key.Account,
                    price = x.Sum(y => y.price)
                }).ToList();
            

            var dataX = result.Select(x => x.dateTime).ToArray();
            var dataY = result.Select(x => x.price).Select(Convert.ToDouble).ToArray();
            return (dataX, dataY);
        }

        public static (string[], Double[]) GetRecordsForPieChart(DateTime begin, DateTime end, List<string> categories, List<string> reasons, List<string> accounts)
        {

            List<Item> records = repository.GetRecords(begin, end);
            var result = records
                .Where(x => (categories.Count == 0 || categories.Contains(x.category)) &&
                            (reasons.Count == 0 || reasons.Contains(x.reason)) &&
                            (accounts.Count == 0 || accounts.Contains(x.account)))
                .GroupBy(x => new
                {
                    Category = categories != null && categories.Any() && categories.Contains(x.category) ? x.category : null,
                    Reason = reasons != null && reasons.Any() && reasons.Contains(x.reason) ? x.reason : null,
                    Account = accounts != null && accounts.Any() && accounts.Contains(x.account) ? x.account : null
                })
                .Select(x => new Item()
                {
                    category = x.Key.Category,
                    reason = x.Key.Reason,
                    account = x.Key.Account,
                    price = x.Sum(y => y.price)
                }).ToList();
            var dataX = result.Select(x => $"{(x.category != null ? x.category + "/" : "")}{(x.reason != null ? x.reason + "/" : "")}{(x.account != null ? x.account + "/" : "")}".TrimEnd('/')).ToArray();
            var dataY = result.Select(x => x.price).Select(Convert.ToDouble).ToArray();
            return (dataX, dataY);
        }

        public static (string[], Double[]) GetRecordsForBarChart(DateTime begin, DateTime end, List<string> categories, List<string> reasons, List<string> accounts)
        {
            return (null, null);
        }
    }
}
