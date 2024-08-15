using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳程式.Models;

namespace 記帳程式.MVP
{
    internal interface INoteFormView
    {
        void LoadRecords(List<Item> list);

        void DeleteRecordFinish(bool isSucess, List<Item> items);

        void EditRecordFinish(bool isSucess, List<Item> items);
        

    }
}
