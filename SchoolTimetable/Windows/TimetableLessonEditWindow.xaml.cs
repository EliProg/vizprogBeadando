using cnTimetable;
using Models;
using SchoolTimetable.Helpers;
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
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for TimetableLessonEditWindow.xaml
    /// </summary>
    public partial class TimetableLessonEditWindow : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly TimetableLesson ttLesson;
        private readonly bool insert;

        public TimetableLessonEditWindow(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();

            var classes = (
                from c in context.Classes
                orderby c.Name
                select new
                {
                    c.Id,
                    c.Name
                }).ToList();
            cbClass.SelectedValuePath = "Id";
            cbClass.DisplayMemberPath = "Name";
            cbClass.ItemsSource = classes;

            var subjects = (
                from s in context.Subjects
                orderby s.Name
                select new
                {
                    s.Id,
                    s.Name
                }).ToList();
            cbSubject.SelectedValuePath = "Id";
            cbSubject.DisplayMemberPath = "Name";
            cbSubject.ItemsSource = subjects;

            var teachers = (
                from u in context.Users
                orderby u.Name
                select new
                {
                    u.Id,
                    u.Name
                }).ToList();
            cbTeacher.SelectedValuePath = "Id";
            cbTeacher.DisplayMemberPath = "Name";
            cbTeacher.ItemsSource = teachers;

            var culture = new System.Globalization.CultureInfo("hu-HU");
            var days = Enumerable.Range(1, 7).Select(x => new
                {
                    Id = (byte)x,
                    Name = Helper.FirstCharToUpper(culture.DateTimeFormat.DayNames[x % 7])
                }).ToList();
            cbDay.SelectedValuePath = "Id";
            cbDay.DisplayMemberPath = "Name";
            cbDay.ItemsSource = days;

            var lessonNums = (
                from s in context.BellSchedules
                orderby s.LessonNum
                select new
                {
                    Id = (byte)s.Id,
                    Name = s.LessonNum.ToString() + ".  " +
                        s.StartTime.ToString("hh\\:mm") + " - " +
                        s.EndTime.ToString("hh\\:mm")
                }).ToList();
            cbLessonNum.SelectedValuePath = "Id";
            cbLessonNum.DisplayMemberPath = "Name";
            cbLessonNum.ItemsSource = lessonNums;

            insert = id == null;
            if (id == null)
            {
                titleBar.Title = "Új óra";
                ttLesson = new TimetableLesson();
                context.Add(ttLesson);
                ttLesson.SchoolYearId = Session.schoolYear.Id;
                ttLesson.StartDate = DateTime.Today;
                ttLesson.EndDate = DateTime.Today;
            }
            else
            {
                titleBar.Title = "Óra módosítása";
                ttLesson = context.TimetableLessons.Find(id);
            }
            this.DataContext = ttLesson;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbClass.SelectedValue == null)
            {
                await UiMessageBox.Show("Az osztály megadása kötelező!", "Hiba");
                return;
            }
            if (cbSubject.SelectedValue == null)
            {
                await UiMessageBox.Show("A tantárgy megadása kötelező!", "Hiba");
                return;
            }
            if (cbTeacher.SelectedValue == null)
            {
                await UiMessageBox.Show("A tanár megadása kötelező!", "Hiba");
                return;
            }
            if (cbDay.SelectedValue == null)
            {
                await UiMessageBox.Show("A nap megadása kötelező!", "Hiba");
                return;
            }
            if (cbLessonNum.SelectedValue == null)
            {
                await UiMessageBox.Show("Az óra megadása kötelező!", "Hiba");
                return;
            }
            if (dpStart.SelectedDate == null)
            {
                await UiMessageBox.Show("A kezdődátum megadása kötelező!", "Hiba");
                return;
            }
            if (dpEnd.SelectedDate == null)
            {
                await UiMessageBox.Show("A végdátum megadása kötelező!", "Hiba");
                return;
            }
            context.SaveChanges();
            Log.Db(insert ? "Insert" : "Update", ttLesson);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
