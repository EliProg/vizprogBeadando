using cnTimetable;
using Microsoft.EntityFrameworkCore;
using Models;
using SchoolTimetable.Helpers;
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

using MessageBoxResult = Wpf.Ui.Controls.MessageBoxResult;

namespace SchoolTimetable.Pages
{
    /// <summary>
    /// Interaction logic for TimetableLessonListPage.xaml
    /// </summary>
    public partial class TimetableLessonListPage : Page
    {
        private void GetList()
        {
            var context = new TimetableContext();
            var lessons = context.TimetableLessons
                .Include(l => l.Class)
                .Include(l => l.Subject)
                .Include(l => l.Teacher)
                .OrderBy(l => l.StartDate)
                .ThenBy(l => l.EndDate)
                .ThenBy(l => l.DayNum)
                .ThenBy(l => l.LessonNum)
                .ToList();
            dgLessons.ItemsSource = lessons;
        }

        public TimetableLessonListPage()
        {
            InitializeComponent();
            GetList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new TimetableLessonEditWindow(null);
            if (window.ShowDialog() == true)
            {
                GetList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var ttLesson = button?.Tag as TimetableLesson;
            var window = new TimetableLessonEditWindow(ttLesson.Id);
            if (window.ShowDialog() == true)
            {
                GetList();
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var ttLesson = button?.Tag as TimetableLesson;
            if (await UiMessageBox.Question("Biztos benne, hogy törli az órát?", "Óra törlése") != MessageBoxResult.Primary)
            {
                return;
            }
            try
            {
                var context = new TimetableContext();
                context.TimetableLessons.Attach(ttLesson);
                context.TimetableLessons.Remove(ttLesson);
                context.SaveChanges();
                Log.Db("Delete", ttLesson);
            }
            catch
            {
                await UiMessageBox.Show("Az óra törlése nem sikerült!", "Hiba");
            }
            GetList();
        }
    }
}
