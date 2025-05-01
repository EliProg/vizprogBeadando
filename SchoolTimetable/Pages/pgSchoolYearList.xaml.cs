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
    /// Interaction logic for pgSchoolYearList.xaml
    /// </summary>
    public partial class pgSchoolYearList : Page
    {
        private readonly TimetableContext _context;

        private void getList()
        {
            var schoolYears = _context.enSchoolYears.ToList();
            dgSchoolYears.ItemsSource = schoolYears;
        }

        public pgSchoolYearList()
        {
            InitializeComponent();
            _context = new TimetableContext();
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var schoolYear = button?.Tag as enSchoolYear;
            if (MessageBox.Show("Biztos benne, hogy törli a tanévet?", "Tanév törlése",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
            _context.Attach(schoolYear);
            _context.enSchoolYears.Remove(schoolYear);
            _context.SaveChanges();
            MessageBox.Show("Az osztály törlése sikerült.");
            getList();
        }
    }
}
