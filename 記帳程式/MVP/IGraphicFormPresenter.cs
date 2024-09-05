using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳程式.MVP
{
    internal interface IGraphicFormPresenter
    {
        void GenerateChart(string chartCategory, DateTime begin, DateTime end, List<string> categorys, List<string> reasons, List<string> accounts);

        void SetView(IGraphicFormView view);
    }
}
