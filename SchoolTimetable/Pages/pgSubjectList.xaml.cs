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

namespace SchoolTimetable.Pages
{
    /// <summary>
    /// Interaction logic for pgSubjectList.xaml
    /// </summary>
    public partial class pgSubjectList : Page
    {
        private void getList()
        {
            var context = new TimetableContext();
            var subjects = context.enSubjects.OrderBy(s => s.Name).ToList();
            dgSubjects.ItemsSource = subjects;
        }

        public pgSubjectList()
        {
            InitializeComponent();
            getList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new wndSubjectEdit(null);
            if (window.ShowDialog() == true)
            {
                getList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var subject = button?.Tag as enSubject;
            var window = new wndSubjectEdit(subject.Id);
            if (window.ShowDialog() == true)
            {
                getList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var subject = button?.Tag as enSubject;
            if (MessageBox.Show("Biztos benne, hogy törli a tantárgyat?", "Tantárgy törlése",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
            try
            {
                var context = new TimetableContext();
                context.enSubjects.Attach(subject);
                context.enSubjects.Remove(subject);
                context.SaveChanges();
                Helper.Log("Delete", subject);
            }
            catch
            {
                MessageBox.Show("A tantárgy törlése nem sikerült!", "Hiba",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            getList();
        }
    }
}
