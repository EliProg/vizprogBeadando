using cnTimetable;
using Models;
using SchoolTimetable.Helpers;
using SchoolTimetable.ViewModels;
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

using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndUserEdit.xaml
    /// </summary>
    public partial class wndUserEdit : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly enUser user;

        public wndUserEdit(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();
            if (id == null)
            {
                titleBar.Title = "Új felhasználó";
                user = new enUser();
                context.Add(user);
            }
            else
            {
                titleBar.Title = "Felhasználó módosítása";
                user = context.enUsers.Find(id);
                tbName.Text = user.Name;
                tbUsername.Text = user.Username;
                cbAdmin.IsChecked = user.Admin;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                MessageBox.Show("A név megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                MessageBox.Show("A felhasználónév megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            user.Name = tbName.Text;
            user.Username = tbUsername.Text;
            user.Admin = cbAdmin.IsChecked == true;
            if (!string.IsNullOrWhiteSpace(pbPassword.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(pbPassword.Password);
            }
            context.SaveChanges();
            Helper.Log("Update", user);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
