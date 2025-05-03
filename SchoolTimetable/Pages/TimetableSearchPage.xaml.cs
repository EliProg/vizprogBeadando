using cnTimetable;
using Microsoft.EntityFrameworkCore;
using SchoolTimetable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for TimetableSearchPage.xaml
    /// </summary>
    public partial class TimetableSearchPage : Page
    {
        public TimetableSearchPage()
        {
            InitializeComponent();

            var context = new TimetableContext();

            var teachers = (
                from t in context.Users
                orderby t.Name
                select new
                {
                    t.Id,
                    t.Name
                }).ToList();
            teachers.Insert(0, new { Id = -1, Name = "Minden" });
            cbTeacher.ItemsSource = teachers;
            cbTeacher.SelectedValuePath = "Id";
            cbTeacher.DisplayMemberPath = "Name";
            cbTeacher.SelectedIndex = 0;

            var classes = (
                from c in context.Classes
                orderby c.Name
                select new
                {
                    c.Id,
                    c.Name
                }).ToList();
            classes.Insert(0, new { Id = -1, Name = "Minden" });
            cbClass.ItemsSource = classes;
            cbClass.SelectedValuePath = "Id";
            cbClass.DisplayMemberPath = "Name";
            cbClass.SelectedIndex = 0;

            dpDate.SelectedDate = DateTime.Today;
            dpDate.DisplayDateStart = Session.schoolYear.StartDate;
            dpDate.DisplayDateEnd = Session.schoolYear.EndDate;
        }

        private void GetList()
        {
            var datetime = dpDate.SelectedDate;
            if (datetime == null)
            {
                return;
            }
            var date = datetime.Value.ToString("yyyyMMdd");
            var context = new TimetableContext();
            var lessons = context.Database.SqlQuery<LessonViewModel>(
                @$"
                set datefirst 1;
                select
                    L.SchoolYearId,
                    L.LoggedLessonId,
                    L.Topic,
                    cast({date} as datetime) Date,
                    Users.Name Teacher,
                    Users.Id TeacherId,
                    Subjects.Name Subject,
                    Subjects.Id SubjectId,
                    Classes.Name Class,
                    Classes.Id ClassId,
                    BellSchedules.LessonNum,
                    BellSchedules.StartTime LessonStart,
                    BellSchedules.EndTime LessonEnd
                from (
                    select
                        LL.Id LoggedLessonId,
                        LL.Topic,
                        coalesce(LL.SchoolYearId, TL.SchoolYearId) SchoolYearId,
                        coalesce(LL.TeacherId, TL.TeacherId) TeacherId,
                        coalesce(LL.SubjectId, TL.SubjectId) SubjectId,
                        coalesce(LL.ClassId, TL.ClassId) ClassId,
                        coalesce(LL.LessonNum, TL.LessonNum) LessonNum
                    from (
                        select *
                        from LoggedLessons
                        where LoggedLessons.SchoolYearId = {Session.schoolYear.Id}
                        and LoggedLessons.Date = {date}
                    ) LL
                    full outer join (
                        select *
                        from TimetableLessons
                        where TimetableLessons.SchoolYearId = {Session.schoolYear.Id}
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
                inner join BellSchedules on L.LessonNum = BellSchedules.LessonNum
                order by BellSchedules.LessonNum
            ").AsEnumerable();
            if (cbClass.SelectedIndex > 0)
            {
                lessons = lessons.Where(l => l.ClassId == int.Parse(cbClass.SelectedValue.ToString()));
            }
            if (cbTeacher.SelectedIndex > 0)
            {
                lessons = lessons.Where(l => l.TeacherId == int.Parse(cbTeacher.SelectedValue.ToString()));
            }
            dgLessons.ItemsSource = lessons.ToList();
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            GetList();
        }

        private void cbTeacher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetList();
        }

        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetList();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            dpDate.SelectedDate = dpDate.SelectedDate.Value.AddDays(-1);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            dpDate.SelectedDate = dpDate.SelectedDate.Value.AddDays(1);
        }

        private void btnToday_Click(object sender, RoutedEventArgs e)
        {
            dpDate.SelectedDate = DateTime.Today;
        }
    }
}
