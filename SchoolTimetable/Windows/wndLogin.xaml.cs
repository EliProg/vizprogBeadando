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

using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndLogin.xaml
    /// </summary>
    public partial class wndLogin : FluentWindow
    {
        private readonly TimetableContext _context;

        public wndLogin()
        {
            InitializeComponent();
            _context = new TimetableContext();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var user = _context.enUsers.FirstOrDefault(x => x.Username == tbUsername.Text && !string.IsNullOrEmpty(x.PasswordHash));
            if (user == null || !BCrypt.Net.BCrypt.Verify(pbPassword.Password, user.PasswordHash))
            {
                //MessageBox.Show("Helytelen felhasználónév vagy jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
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
