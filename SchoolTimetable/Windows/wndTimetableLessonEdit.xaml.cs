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
    /// Interaction logic for wndTimetableLessonEdit.xaml
    /// </summary>
    public partial class wndTimetableLessonEdit : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly enTimetableLesson ttLesson;
        private readonly bool insert;

        public wndTimetableLessonEdit(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();

            var classes = (
                from t in context.enClasses
                orderby t.Name
                select new
                {
                    t.Id,
                    t.Name
                }).ToList();
            cbClass.SelectedValuePath = "Id";
            cbClass.DisplayMemberPath = "Name";
            cbClass.ItemsSource = classes;

            var subjects = (
                from t in context.enSubjects
                orderby t.Name
                select new
                {
                    t.Id,
                    t.Name
                }).ToList();
            cbSubject.SelectedValuePath = "Id";
            cbSubject.DisplayMemberPath = "Name";
            cbSubject.ItemsSource = subjects;

            var teachers = (
                from t in context.enUsers
                orderby t.Name
                select new
                {
                    t.Id,
                    t.Name
                }).ToList();
            cbTeacher.SelectedValuePath = "Id";
            cbTeacher.DisplayMemberPath = "Name";
            cbTeacher.ItemsSource = teachers;

            var culture = new System.Globalization.CultureInfo("hu-HU");
            var days = Enumerable.Range(1, 7).Select(x => new
                {
                    Id = x,
                    Name = culture.DateTimeFormat.DayNames[x % 7]
                }).ToList();
            cbDay.SelectedValuePath = "Id";
            cbDay.DisplayMemberPath = "Name";
            cbDay.ItemsSource = days;

            var lessonNums = (
                from t in context.enLessonSchedules
                orderby t.LessonNum
                select new
                {
                    t.Id,
                    Name = t.LessonNum.ToString() + ".  " +
                        t.StartTime.ToString("hh\\:mm") + " - " +
                        t.EndTime.ToString("hh\\:mm")
                }).ToList();
            cbLessonNum.SelectedValuePath = "Id";
            cbLessonNum.DisplayMemberPath = "Name";
            cbLessonNum.ItemsSource = lessonNums;

            insert = id == null;
            if (id == null)
            {
                titleBar.Title = "Új óra";
                ttLesson = new enTimetableLesson();
                context.Add(ttLesson);
                ttLesson.SchoolYearId = Session.schoolYear.Id;
                ttLesson.StartDate = DateTime.Today;
                ttLesson.EndDate = DateTime.Today;
            }
            else
            {
                titleBar.Title = "Óra módosítása";
                ttLesson = context.enTimetableLessons.Find(id);
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
