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
    /// Interaction logic for pgSchoolYearList.xaml
    /// </summary>
    public partial class pgSchoolYearList : Page
    {
        private void getList()
        {
            var context = new TimetableContext();
            var schoolYears = context.enSchoolYears.OrderBy(y => y.Name).ToList();
            dgSchoolYears.ItemsSource = schoolYears;
        }

        public pgSchoolYearList()
        {
            InitializeComponent();
            getList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new wndSchoolYearEdit(null);
            if (window.ShowDialog() == true)
            {
                getList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var schoolYear = button?.Tag as enSchoolYear;
            var window = new wndSchoolYearEdit(schoolYear.Id);
            if (window.ShowDialog() == true)
            {
                getList();
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var schoolYear = button?.Tag as enSchoolYear;
            if (await UiMessageBox.Question("Biztos benne, hogy törli a tanévet?", "Tanév törlése") != MessageBoxResult.Primary)
            {
                return;
            }
            try
            {
                var context = new TimetableContext();
                context.enSchoolYears.Attach(schoolYear);
                context.enSchoolYears.Remove(schoolYear);
                context.SaveChanges();
                Log.Db("Delete", schoolYear);
            }
            catch
            {
                await UiMessageBox.Show("A tanév törlése nem sikerült!", "Hiba");
            }
            getList();
        }
    }
}
