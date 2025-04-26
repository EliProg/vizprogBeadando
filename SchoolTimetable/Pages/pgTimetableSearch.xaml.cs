using cnTimetable;
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

namespace SchoolTimetable.Pages
{
    /// <summary>
    /// Interaction logic for pgTimetableSearch.xaml
    /// </summary>
    public partial class pgTimetableSearch : Page
    {
        TimetableContext _context;

        public pgTimetableSearch()
        {
            _context = new TimetableContext();
            InitializeComponent();

            var teachers = (
                from t in _context.enUsers
                select new
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList();
            cbTeacher.ItemsSource = teachers;
            cbTeacher.SelectedValuePath = "Id";
            cbTeacher.DisplayMemberPath = "Name";

            var classes = (
                from c in _context.enClasses
                select new
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
            cbClass.ItemsSource = classes;
            cbClass.SelectedValuePath = "Id";
            cbClass.DisplayMemberPath = "Name";
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbTeacher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
