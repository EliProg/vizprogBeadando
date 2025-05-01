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
using cnTimetable;
using Microsoft.EntityFrameworkCore;
using Models;
using SchoolTimetable.Helpers;

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndLessonLog.xaml
    /// </summary>
    public partial class wndLessonLog : Window
    {
        TimetableContext _context;
        LessonViewModel lesson;

        public wndLessonLog(LessonViewModel lesson)
        {
            InitializeComponent();
            _context = new TimetableContext();
            this.lesson = lesson;
            tbClass.Text = lesson.Class;
            tbDate.Text = lesson.Date.ToString("yyyy. MM. dd.");
            tbLessonNum.Text = lesson.LessonNum.ToString();
            tbSubject.Text = lesson.Subject;
            tbTeacher.Text = lesson.Teacher;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
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
                    SchoolYearId = 1,
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
