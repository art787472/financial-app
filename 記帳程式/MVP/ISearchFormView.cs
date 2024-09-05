using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳程式.Models;

namespace 記帳程式.MVP
{
    internal interface ISearchFormView
    {
        void SearchRecordsFinish(List<Item> list);
    }
}
