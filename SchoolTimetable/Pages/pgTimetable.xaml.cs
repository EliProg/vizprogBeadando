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

namespace SchoolTimetable.Pages
{
    /// <summary>
    /// Interaction logic for pgTimetable.xaml
    /// </summary>
    public partial class pgTimetable : Page
    {
        TimetableContext _context;

        int userId;

        private class LessonViewModel
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public decimal? EduId { get; set; }
            public string Name { get; set; }
            public bool Admin { get; set; }
        }

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
                select *
                from (
                    select *
                    from LoggedLesson
                    inner join LessonSchedule on LoggedLesson.LessonNum = LessonSchedule.Id
                    where LoggedLesson.SchoolYearId = {userId}
                    and LoggedLesson.TeacherId = {userId}
                    and LoggedLesson.Date = {date}
                ) LoggedLesson
                full outer join (
                    select *
                    from TimetableLesson
                    inner join LessonSchedule on TimetableLesson.LessonNum = LessonSchedule.Id
                    where TimetableLesson.SchoolYearId = {userId}
                    and TimetableLesson.TeacherId = {userId}
                    and TimetableLesson.StartDate <= {date}
                    and TimetableLesson.EndDate >= {date}
                ) TimetableLesson on
                    TimetableLesson.SubjectId = LoggedLesson.SubjectId
                    and TimetableLesson.ClassId = LoggedLesson.ClassId
                    and TimetableLesson.DayNum = datepart(dw, LoggedLesson.Date)
                    and TimetableLesson.LessonNum = LoggedLesson.LessonNum
                ").ToList();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            getList();
        }
    }
}
