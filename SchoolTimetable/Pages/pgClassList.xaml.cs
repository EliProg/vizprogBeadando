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
    /// Interaction logic for pgClassList.xaml
    /// </summary>
    public partial class pgClassList : Page
    {
        private void getList()
        {
            var context = new TimetableContext();
            var classes = context.enClasses.OrderBy(c => c.Name).ToList();
            dgClasses.ItemsSource = classes;
        }

        public pgClassList()
        {
            InitializeComponent();
            getList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new wndClassEdit(null);
            if (window.ShowDialog() == true)
            {
                getList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var _class = button?.Tag as enClass;
            var window = new wndClassEdit(_class.Id);
            if (window.ShowDialog() == true)
            {
                getList();
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var _class = button?.Tag as enClass;
            if (await UiMessageBox.Question("Biztos benne, hogy törli az osztályt?", "Osztály törlése") != MessageBoxResult.Primary)
            {
                return;
            }
            try
            {
                var context = new TimetableContext();
                context.enClasses.Attach(_class);
                context.enClasses.Remove(_class);
                context.SaveChanges();
                Log.Db("Delete", _class);
            }
            catch
            {
                await UiMessageBox.Show("Az osztály törlése nem sikerült!", "Hiba");
            }
            getList();
        }
    }
}
