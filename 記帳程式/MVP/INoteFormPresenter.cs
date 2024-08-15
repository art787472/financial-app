using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳程式.Models;

namespace 記帳程式.MVP
{
    internal interface INoteFormPresenter
    {
        void LoadRecords(DateTime startTime, DateTime endTime);

        void DeleteRecord(int idx);

        void EditRecord(int rowIdx, int colIdx, string editedData);

        void SetView(INoteFormView view);
    }
}
