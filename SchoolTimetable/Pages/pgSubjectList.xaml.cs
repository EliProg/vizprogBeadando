using cnTimetable;
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
    /// Interaction logic for pgSubjectList.xaml
    /// </summary>
    public partial class pgSubjectList : Page
    {
        TimetableContext _context;
        private class SubjectViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private void getList()
        {
            var lekerdezes = (from s in _context.enSubjects
                              select new SubjectViewModel
                              {
                                  Id = s.Id,
                                  Name = s.Name
                              }).ToList();
            dgSubjects.ItemsSource = lekerdezes;
        }

      
        public pgSubjectList()
        {
            InitializeComponent();
            _context = new TimetableContext();
            getList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new wndSubjectEdit(0);
            window.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var subject = button?.Tag as SubjectViewModel;
            var window = new wndSubjectEdit(subject.Id);
            window.ShowDialog();
            getList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var subject = button?.Tag as SubjectViewModel;

            var subjectToDelete = _context.enSubjects.FirstOrDefault(u => u.Id == subject.Id);
            _context.enSubjects.Remove(subjectToDelete);
            _context.SaveChanges();
        }
    }
}
