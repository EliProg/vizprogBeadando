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
    /// Interaction logic for SubjectListPage.xaml
    /// </summary>
    public partial class SubjectListPage : Page
    {
        private void GetList()
        {
            var context = new TimetableContext();
            var subjects = context.Subjects.OrderBy(s => s.Name).ToList();
            dgSubjects.ItemsSource = subjects;
        }

        public SubjectListPage()
        {
            InitializeComponent();
            GetList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new SubjectEditWindow(null);
            if (window.ShowDialog() == true)
            {
                GetList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var subject = button?.Tag as Subject;
            var window = new SubjectEditWindow(subject.Id);
            if (window.ShowDialog() == true)
            {
                GetList();
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var subject = button?.Tag as Subject;
            if (await UiMessageBox.Question("Biztos benne, hogy törli a tantárgyat?", "Tantárgy törlése") != MessageBoxResult.Primary)
            {
                return;
            }
            try
            {
                var context = new TimetableContext();
                context.Subjects.Attach(subject);
                context.Subjects.Remove(subject);
                context.SaveChanges();
                Log.Db("Delete", subject);
            }
            catch
            {
                await UiMessageBox.Show("A tantárgy törlése nem sikerült!", "Hiba");
            }
            GetList();
        }
    }
}
