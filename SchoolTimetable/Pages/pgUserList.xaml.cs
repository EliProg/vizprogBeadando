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
    /// Interaction logic for pgUserList.xaml
    /// </summary>
    public partial class pgUserList : Page
    {
        private void getList()
        {
            var context = new TimetableContext();
            var users = context.enUsers.OrderBy(u => u.Name).ToList();
            dgUsers.ItemsSource = users;
        }

        public pgUserList()
        {
            InitializeComponent();
            getList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var window = new wndUserEdit(null);
            if (window.ShowDialog() == true)
            {
                getList();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button?.Tag as enUser;
            var window = new wndUserEdit(user.Id);
            if (window.ShowDialog() == true)
            {
                getList();
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button?.Tag as enUser;
            if (await UiMessageBox.Question("Biztos benne, hogy törli a felhasználót?", "Felhasználó törlése") != MessageBoxResult.Primary)
            {
                return;
            }
            try
            {
                var context = new TimetableContext();
                context.enUsers.Attach(user);
                context.enUsers.Remove(user);
                context.SaveChanges();
                Log.Db("Delete", user);
            }
            catch
            {
                await UiMessageBox.Show("A felhasználó törlése nem sikerült!", "Hiba");
            }
            getList();
        }
    }
}
