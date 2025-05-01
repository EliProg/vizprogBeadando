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
    /// Interaction logic for pgUserList.xaml
    /// </summary>
    public partial class pgUserList : Page
    {
        private readonly TimetableContext _context;

        private void getList()
        {
            var users = _context.enUsers.ToList();
            dgUsers.ItemsSource = users;
        }

        public pgUserList()
        {
            InitializeComponent();
            _context = new TimetableContext();
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button?.Tag as enUser;
            if (MessageBox.Show("Biztos benne, hogy törli a felhasználót?", "Felhasználó törlése",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
            _context.Attach(user);
            _context.enUsers.Remove(user);
            _context.SaveChanges();
            MessageBox.Show("A felhasználó törlése sikerült.");
            getList();
        }
    }
}
