using cnTimetable;
using Models;
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
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndUserEdit.xaml
    /// </summary>
    public partial class wndUserEdit : FluentWindow
    {
        private readonly TimetableContext _context;
        private readonly enUser user;

        public wndUserEdit(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _context = new TimetableContext();
            if (id == null)
            {
                user = new enUser();
                _context.Add(user);
                titleBar.Title = "Felhasználó létrehozása";
                btnSave.Content = "Létrehozás";
            }
            else
            {
                user = _context.enUsers.Find(id);
                tbName.Text = user.Name;
                tbUsername.Text = user.Username;
                cbAdmin.IsChecked = user.Admin;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                //MessageBox.Show("A név megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                //MessageBox.Show("A felhasználónév megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            user.Name = tbName.Text;
            user.Username = tbUsername.Text;
            user.Admin = cbAdmin.IsChecked == true;
            if (!string.IsNullOrWhiteSpace(pbPassword.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(pbPassword.Password);
            }
            _context.SaveChanges();
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
