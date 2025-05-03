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
    /// Interaction logic for ClassListPage.xaml
    /// </summary>
    public partial class ClassListPage : Page
    {
        private void GetList()
        {
            var context = new TimetableContext();
            var classes = context.Classes.OrderBy(c => c.Name).ToList();
            dgClasses.ItemsSource = classes;
        }

        public ClassListPage()
        {
            InitializeComponent();
            GetList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new ClassEditWindow(null);
            if (window.ShowDialog() == true)
            {
                GetList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var _class = button?.Tag as Class;
            var window = new ClassEditWindow(_class.Id);
            if (window.ShowDialog() == true)
            {
                GetList();
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var _class = button?.Tag as Class;
            if (await UiMessageBox.Question("Biztos benne, hogy törli az osztályt?", "Osztály törlése") != MessageBoxResult.Primary)
            {
                return;
            }
            try
            {
                var context = new TimetableContext();
                context.Classes.Attach(_class);
                context.Classes.Remove(_class);
                context.SaveChanges();
                Log.Db("Delete", _class);
            }
            catch
            {
                await UiMessageBox.Show("Az osztály törlése nem sikerült!", "Hiba");
            }
            GetList();
        }
    }
}
