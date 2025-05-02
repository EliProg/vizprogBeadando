using cnTimetable;
using Microsoft.EntityFrameworkCore;
using Models;
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
        private void getList()
        {
            var context = new TimetableContext();
            var lessons = context.enTimetableLessons
                .Include(l => l.enClass)
                .Include(l => l.enSubject)
                .Include(l => l.enUser)
                .OrderBy(l => l.StartDate)
                .ThenBy(l => l.EndDate)
                .ThenBy(l => l.DayNum)
                .ThenBy(l => l.LessonNum)
                .ToList();
            dgLessons.ItemsSource = lessons;
        }

        public pgTimetableLessonList()
        {
            InitializeComponent();
            getList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new wndTimetableLessonEdit(null);
            if (window.ShowDialog() == true)
            {
                getList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var ttLesson = button?.Tag as enTimetableLesson;
            var window = new wndTimetableLessonEdit(ttLesson.Id);
            if (window.ShowDialog() == true)
            {
                getList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var ttLesson = button?.Tag as enTimetableLesson;
            if (MessageBox.Show("Biztos benne, hogy törli az órát?", "Óra törlése",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
            var context = new TimetableContext();
            context.enTimetableLessons.Attach(ttLesson);
            context.enTimetableLessons.Remove(ttLesson);
            context.SaveChanges();
            getList();
        }
    }
}
