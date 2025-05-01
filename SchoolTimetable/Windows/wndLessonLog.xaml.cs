using cnTimetable;
using Models;
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

using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndLessonLog.xaml
    /// </summary>
    public partial class wndLessonLog : FluentWindow
    {
        private readonly TimetableContext _context;
        private readonly vmLesson lesson;

        public wndLessonLog(vmLesson lesson)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _context = new TimetableContext();
            this.lesson = lesson;
            tbClass.Text = lesson.Class;
            tbDate.Text = lesson.Date.ToString("yyyy. MM. dd.");
            tbLessonNum.Text = lesson.LessonNum.ToString();
            tbSubject.Text = lesson.Subject;
            tbTeacher.Text = lesson.Teacher;
            tbTopic.Text = lesson.Topic;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTopic.Text))
            {
                //MessageBox.Show("Az óra témája nem lehet üres!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (lesson.LoggedLessonId != null)
            {
                var loggedLesson = _context.enLoggedLessons.Find(lesson.LoggedLessonId);
                loggedLesson.Topic = tbTopic.Text;
                _context.SaveChanges();
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
                _context.Add(loggedLesson);
                _context.SaveChanges();
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
