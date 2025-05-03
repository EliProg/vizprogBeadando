using cnTimetable;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SchoolTimetable
{
    internal static class Session
    {
        public static User? user;
        public static SchoolYear? schoolYear;

        public static void Clear()
        {
            user = null;
            schoolYear = null;
        }

        public static void UpdateSchoolYear()
        {
            var context = new TimetableContext();
            schoolYear = context.SchoolYears.OrderByDescending(y => y.Active).ThenByDescending(y => y.StartDate).First();
        }
    }
}
