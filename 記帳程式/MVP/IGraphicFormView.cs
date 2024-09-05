using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace 記帳程式.MVP
{
    internal interface IGraphicFormView
    {
        void GenerateChartFinish(List<Series> series);
        
    }
}
