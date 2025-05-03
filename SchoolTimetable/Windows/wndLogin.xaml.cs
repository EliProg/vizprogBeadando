using cnTimetable;
using SchoolTimetable.Helpers;
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
    /// Interaction logic for wndLogin.xaml
    /// </summary>
    public partial class wndLogin : FluentWindow
    {
        public wndLogin()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var context = new TimetableContext();
            var user = context.enUsers.FirstOrDefault(u => u.Username == tbUsername.Text && !string.IsNullOrEmpty(u.PasswordHash));
            if (user == null || !BCrypt.Net.BCrypt.Verify(pbPassword.Password, user.PasswordHash))
            {
                Log.Login("Failed", user);
                await UiMessageBox.Show("Helytelen felhasználónév vagy jelszó!", "Hiba");
            }
            else
            {
                Log.Login("Successful", user);
                Session.user = user;
                Session.updateSchoolYear();
                var window = new wndMain
                {
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = this
                };
                window.Show();
                window.Owner = null;
                Application.Current.MainWindow = window;
                this.Close();
            }
        }
    }
}
