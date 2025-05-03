using cnTimetable;
using Models;
using SchoolTimetable.Helpers;
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
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndLessonLog.xaml
    /// </summary>
    public partial class wndLessonLog : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly vmLesson lesson;

        public wndLessonLog(vmLesson entity)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();
            lesson = entity;
            tbClass.Text = lesson.Class;
            tbDate.Text = lesson.Date.ToString("yyyy. MM. dd.") + " " +
                lesson.LessonStart.ToString("hh\\:mm") + "-" +
                lesson.LessonEnd.ToString("hh\\:mm") +
                " (" + lesson.LessonNum.ToString() + ". óra)";
            tbSubject.Text = lesson.Subject;
            tbTeacher.Text = lesson.Teacher;
            tbTopic.Text = lesson.Topic;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTopic.Text))
            {
                await UiMessageBox.Show("Az óra témájának megadása kötelező!", "Hiba");
                return;
            }
            if (lesson.LoggedLessonId != null)
            {
                var loggedLesson = context.enLoggedLessons.Find(lesson.LoggedLessonId);
                loggedLesson.Topic = tbTopic.Text;
                context.SaveChanges();
                Log.Db("Update", loggedLesson);
            }
            else
            {
                var loggedLesson = new enLoggedLesson
                {
                    SubjectId = lesson.SubjectId,
                    TeacherId = lesson.TeacherId,
                    ClassId = lesson.ClassId,
                    SchoolYearId = lesson.SchoolYearId,
                    LessonNum = lesson.LessonNum,
                    Date = lesson.Date,
                    Topic = tbTopic.Text
                };
                context.Add(loggedLesson);
                context.SaveChanges();
                Log.Db("Insert", loggedLesson);
            }
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
