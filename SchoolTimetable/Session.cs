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
        private static TimetableContext _context = new();
        public static enUser? user;
        public static enSchoolYear? schoolYear;

        public static void clear()
        {
            user = null;
            schoolYear = null;
        }

        public static void updateSchoolYear()
        {
            schoolYear = _context.enSchoolYears.OrderByDescending(y => y.Active).ThenByDescending(y => y.StartDate).First();
        }
    }
}
