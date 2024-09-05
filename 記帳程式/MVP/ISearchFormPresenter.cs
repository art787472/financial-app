using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳程式.MVP
{
    internal interface ISearchFormPresenter
    {
        void SearchRecords(DateTime begin, DateTime end, List<string> categorys, List<string> reasons, List<string> accounts);

        void SetView(ISearchFormView form);
    }
}
