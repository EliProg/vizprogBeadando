using Microsoft.EntityFrameworkCore;
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
using cnTimetable;
using Models;

namespace Viz_Projekt_Feladat
{
    /// <summary>
    /// Interaction logic for UserListPage.xaml
    /// </summary>
    public partial class UserListPage : Page
    {
        TimetableContext _context;
        public class UserViewModel
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public decimal? EduId { get; set; }
            public string Name { get; set; }
            public bool Admin { get; set; }
        }

        public void getList()
        {
            var lekerdezes = (from s in _context.enUsers
                              select new UserViewModel
                              {
                                  Id = s.Id,
                                  Username = s.Username,
                                  EduId = s.EduId,
                                  Name = s.Name,
                                  Admin = s.Admin
                              }).ToList();
            dgUsers.ItemsSource = lekerdezes;
        }
        
        public UserListPage()
        {
            InitializeComponent();
            _context = new TimetableContext();
            getList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            // Get the row's bound data (UserViewModel) from the button's Tag
            var user = button?.Tag as UserViewModel;
            var window = new UserEditWindow(user.Id);
            window.ShowDialog();
            getList();

// MessageBox.Show($"{user.Id}");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            // Get the row's bound data (UserViewModel) from the button's Tag
            var user = button?.Tag as UserViewModel;

            var userToDelete = _context.enUsers.FirstOrDefault(u => u.Id == user.Id);
            _context.enUsers.Remove(userToDelete);
            _context.SaveChanges();
            MessageBox.Show("User deleted successfully.");

            // MessageBox.Show("Törlés");
        }
    }
}
