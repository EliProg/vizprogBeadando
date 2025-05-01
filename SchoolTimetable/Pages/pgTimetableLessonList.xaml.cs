using cnTimetable;
using Microsoft.EntityFrameworkCore;
using SchoolTimetable.Windows;
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
    /// Interaction logic for pgTimetableLessonList.xaml
    /// </summary>
    public partial class pgTimetableLessonList : Page
    {
        TimetableContext _context;
        private class TableViewModel
        {
            public int Id { get; set; }
            public string Subject { get; set; }
            public string? Teacher { get; set; }
            public string Class { get; set; }
            public int Day { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public int LessonNum { get; set; }
        }
        private void getList()
        {
            var lekerdezes = (from t in _context.enTimetableLessons
                              join u in _context.enUsers on t.TeacherId equals u.Id
                              join s in _context.enSubjects on t.SubjectId equals s.Id
                              join c in _context.enClasses on t.ClassId equals c.Id

                              select new TableViewModel
                              {
                                  Id = t.Id,
                                  Subject = s.Name,
                                  Teacher = u.Name,
                                  Class = c.Name,
                                  Day = t.DayNum,
                                  Start = t.StartDate,
                                  End = t.EndDate,
                                  LessonNum = t.LessonNum
                              }).ToList();
            dgUsers.ItemsSource = lekerdezes;
        }

        public pgTimetableLessonList()
        {
            InitializeComponent();
            _context = new TimetableContext();
            getList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new wndTimetableLessonEdit(0);
            window.ShowDialog();
            getList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var table = button?.Tag as TableViewModel;
            var window = new wndTimetableLessonEdit(table.Id);
            window.ShowDialog();
            getList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var table = button?.Tag as TableViewModel;

            var tableToDelete = _context.enTimetableLessons.FirstOrDefault(u => u.Id == table.Id);
            _context.enTimetableLessons.Remove(tableToDelete);
            _context.SaveChanges();
        }
    }
    }

