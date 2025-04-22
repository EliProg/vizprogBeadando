using cnTimetable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SchoolTimetable.Helpers;

namespace SchoolTimetable.Pages
{
    /// <summary>
    /// Interaction logic for pgTimetable.xaml
    /// </summary>
    public partial class pgTimetable : Page
    {
        TimetableContext _context;

        int userId;

        public pgTimetable(int userId)
        {
            InitializeComponent();
            _context = new TimetableContext();
            this.userId = userId;
        }

        private void getList()
        {
            var datetime = dpDate.SelectedDate;
            var date = datetime.Value.Date.ToString("yyyy-MM-dd");
            var lessons = _context.Database.SqlQuery<LessonViewModel>(
                @$"
                select
                    coalesce(LL.TeacherId, TL.TeacherId) TeacherId,
                    coalesce(LL.SubjectId, TL.SubjectId) SubjectId,
                    coalesce(LL.ClassId, TL.ClassId) ClassId,
                    coalesce(LL.LessonNum, TL.LessonNum) LessonNum
                from (
                    select *
                    from LoggedLesson
                    inner join LessonSchedule on LoggedLesson.LessonNum = LessonSchedule.Id
                    where LoggedLesson.SchoolYearId = {userId}
                    and LoggedLesson.TeacherId = {userId}
                    and LoggedLesson.Date = {date}
                ) LL
                full outer join (
                    select *
                    from TimetableLesson
                    inner join LessonSchedule on TimetableLesson.LessonNum = LessonSchedule.Id
                    where TimetableLesson.SchoolYearId = {userId}
                    and TimetableLesson.TeacherId = {userId}
                    and TimetableLesson.StartDate <= {date}
                    and TimetableLesson.EndDate >= {date}
                ) TL on
                    TL.SubjectId = LL.SubjectId
                    and TL.ClassId = LL.ClassId
                    and TL.DayNum = datepart(dw, LL.Date)
                    and TL.LessonNum = LL.LessonNum
                ").ToList();
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            getList();
        }
    }
}
