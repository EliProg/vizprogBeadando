using cnTimetable;
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
    /// Interaction logic for BellScheduleListPage.xaml
    /// </summary>
    public partial class BellScheduleListPage : Page
    {
        private void GetList()
        {
            var context = new TimetableContext();
            var bellSchedules = context.BellSchedules.OrderBy(l => l.LessonNum).ToList();
            dgBellSchedules.ItemsSource = bellSchedules;
        }

        public BellScheduleListPage()
        {
            InitializeComponent();
            GetList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var bellSchedule = button?.Tag as BellSchedule;
            var window = new BellScheduleEditWindow(bellSchedule.Id);
            if (window.ShowDialog() == true)
            {
                GetList();
            }
        }
    }
}
