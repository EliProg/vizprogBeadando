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
    /// Interaction logic for pgClassList.xaml
    /// </summary>
    public partial class pgClassList : Page
    {
        TimetableContext _context;
        private class ClassViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private void getList()
        {
            var lekerdezes = (from s in _context.enClasses
                              select new ClassViewModel
                              {
                                  Id = s.Id,
                                  Name = s.Name
                              }).ToList();
            dgClasses.ItemsSource = lekerdezes;
        }

        public pgClassList()
        {
            InitializeComponent();
            _context = new TimetableContext();
            getList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new wndClassEdit(0);
            window.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Class = button?.Tag as ClassViewModel;
            var window = new wndClassEdit(Class.Id);
            window.ShowDialog();
            getList();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Class = button?.Tag as ClassViewModel;

            var classToDelete = _context.enClasses.FirstOrDefault(u => u.Id == Class.Id);
            _context.enClasses.Remove(classToDelete);
            _context.SaveChanges();
        }
    }
}
