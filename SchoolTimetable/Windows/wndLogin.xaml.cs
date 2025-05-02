using cnTimetable;
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
    /// Interaction logic for wndLogin.xaml
    /// </summary>
    public partial class wndLogin : FluentWindow
    {
        public wndLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var context = new TimetableContext();
            var user = context.enUsers.FirstOrDefault(x => x.Username == tbUsername.Text && !string.IsNullOrEmpty(x.PasswordHash));
            if (user == null || !BCrypt.Net.BCrypt.Verify(pbPassword.Password, user.PasswordHash))
            {
                /*
                var msg = new MessageBox();
                msg.Title = "ABC";
                msg.Content = "Hello";
                msg.PrimaryButtonText = "OK";
                msg.ShowDialogAsync();
                */
                MessageBox.Show("Helytelen felhasználónév vagy jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Session.user = user;
                Session.updateSchoolYear();
                var window = new wndMain();
                window.Show();
                Application.Current.MainWindow = window;
                this.Close();
            }
        }
    }
}
