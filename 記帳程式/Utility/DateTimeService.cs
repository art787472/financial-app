using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳程式.Utility
{
    internal class DateTimeService
    {
        public static DateTime GetFirstDayOfThisMonth() => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public static DateTime GetLastDayOfThisMonth()
        {
            var monthDays = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1) - new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, monthDays.Days);
            return end;
        }

    }
}
