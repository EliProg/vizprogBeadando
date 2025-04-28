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
using System.Collections;
using SchoolTimetable.Windows;

namespace SchoolTimetable.Pages
{
    /// <summary>
    /// Interaction logic for pgTimetable.xaml
    /// </summary>
    public partial class pgTimetable : Page
    {
        TimetableContext _context;

        int userId;
        int schoolYearId;

        public pgTimetable(int userId, int schoolYearId)
        {
            InitializeComponent();
            _context = new TimetableContext();
            this.userId = userId;
            this.schoolYearId = schoolYearId;
            dpDate.SelectedDate = DateTime.Today;
        }

        private void getList()
        {
            var datetime = dpDate.SelectedDate;
            if (datetime == null)
            {
                return;
            }
            var date = datetime.Value.ToString("yyyyMMdd");
            var lessons = _context.Database.SqlQuery<LessonViewModel>(
                @$"
                set datefirst 1;
                select
                    L.LoggedLessonId,
                    L.Topic,
                    cast({date} as datetime) Date,
                    Users.Name Teacher,
                    Users.Id TeacherId,
                    Subjects.Name Subject,
                    Subjects.Id SubjectId,
                    Classes.Name Class,
                    Classes.Id ClassId,
                    LessonSchedules.LessonNum,
                    LessonSchedules.StartTime LessonStart,
                    LessonSchedules.EndTime LessonEnd
                from (
                    select
                        LL.Id LoggedLessonId,
                        LL.Topic,
                        coalesce(LL.TeacherId, TL.TeacherId) TeacherId,
                        coalesce(LL.SubjectId, TL.SubjectId) SubjectId,
                        coalesce(LL.ClassId, TL.ClassId) ClassId,
                        coalesce(LL.LessonNum, TL.LessonNum) LessonNum
                    from (
                        select *
                        from LoggedLessons
                        where LoggedLessons.SchoolYearId = {schoolYearId}
                        and LoggedLessons.TeacherId = {userId}
                        and LoggedLessons.Date = {date}
                    ) LL
                    full outer join (
                        select *
                        from TimetableLessons
                        where TimetableLessons.SchoolYearId = {schoolYearId}
                        and TimetableLessons.TeacherId = {userId}
                        and TimetableLessons.DayNum = datepart(dw, {date})
                        and TimetableLessons.StartDate <= {date}
                        and TimetableLessons.EndDate >= {date}
                    ) TL on
                        TL.SubjectId = LL.SubjectId
                        and TL.ClassId = LL.ClassId
                        and TL.LessonNum = LL.LessonNum
                ) L
                inner join Users on L.TeacherId = Users.Id
                inner join Subjects on L.SubjectId = Subjects.Id
                inner join Classes on L.ClassId = Classes.Id
                inner join LessonSchedules on L.LessonNum = LessonSchedules.LessonNum
            ").ToList();
            dgLessons.ItemsSource = lessons;
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            getList();
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var lesson = button?.Tag as LessonViewModel;
            var window = new wndLessonLog(lesson);
            window.ShowDialog();
            getList();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            dpDate.SelectedDate = dpDate.SelectedDate.Value.AddDays(-1);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            dpDate.SelectedDate = dpDate.SelectedDate.Value.AddDays(1);
        }
    }
}
