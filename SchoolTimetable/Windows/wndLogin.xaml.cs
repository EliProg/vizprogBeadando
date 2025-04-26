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

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndLogin.xaml
    /// </summary>
    public partial class wndLogin : Window
    {
        public wndLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (TimetableContext _context = new())
            {
                var user = _context.enUsers.FirstOrDefault(x => x.Username == tbUsername.Text);
                if (user == null || !BCrypt.Net.BCrypt.Verify(tbPassword.Text, user.PasswordHash))
                {
                    MessageBox.Show("Helytelen felhasználónév vagy jelszó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var w = new wndMain(user);
                    w.Show();
                    this.Close();
                }
            }
        }
    }
}
