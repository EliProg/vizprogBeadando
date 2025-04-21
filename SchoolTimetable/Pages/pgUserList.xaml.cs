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
using SchoolTimetable.Windows;

namespace SchoolTimetable.Pages
{
    /// <summary>
    /// Interaction logic for pgUserList.xaml
    /// </summary>
    public partial class pgUserList : Page
    {
        TimetableContext _context;

        private class UserViewModel
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public decimal? EduId { get; set; }
            public string Name { get; set; }
            public bool Admin { get; set; }
        }

        private void getList()
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

        public pgUserList()
        {
            InitializeComponent();
            _context = new TimetableContext();
            getList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            // Get the row's bound data (UserViewModel) from the button's Tag
            var user = button?.Tag as UserViewModel;
            var window = new wndUserEdit(user.Id);
            window.ShowDialog();
            getList();
            // MessageBox.Show($"{user.Id}");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
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
