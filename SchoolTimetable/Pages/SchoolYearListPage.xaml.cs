using cnTimetable;
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
    /// Interaction logic for SchoolYearListPage.xaml
    /// </summary>
    public partial class SchoolYearListPage : Page
    {
        private void GetList()
        {
            var context = new TimetableContext();
            var schoolYears = context.SchoolYears.OrderBy(y => y.Name).ToList();
            dgSchoolYears.ItemsSource = schoolYears;
        }

        public SchoolYearListPage()
        {
            InitializeComponent();
            GetList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new SchoolYearEditWindow(null);
            if (window.ShowDialog() == true)
            {
                GetList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var schoolYear = button?.Tag as SchoolYear;
            var window = new SchoolYearEditWindow(schoolYear.Id);
            if (window.ShowDialog() == true)
            {
                GetList();
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var schoolYear = button?.Tag as SchoolYear;
            if (await UiMessageBox.Question("Biztos benne, hogy törli a tanévet?", "Tanév törlése") != MessageBoxResult.Primary)
            {
                return;
            }
            try
            {
                var context = new TimetableContext();
                context.SchoolYears.Attach(schoolYear);
                context.SchoolYears.Remove(schoolYear);
                context.SaveChanges();
                Log.Db("Delete", schoolYear);
            }
            catch
            {
                await UiMessageBox.Show("A tanév törlése nem sikerült!", "Hiba");
            }
            GetList();
        }
    }
}
