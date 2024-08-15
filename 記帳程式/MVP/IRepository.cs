using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳程式.Models;

namespace 記帳程式.MVP
{
    internal interface IRepository
    {
        
        List<Item> GetRecords(DateTime startTime, DateTime endTime);

        void AddRecord(Item data);

        void DeleteRecord(List<Item> list);

        void EditRecord(List<Item> list);

    }
}
