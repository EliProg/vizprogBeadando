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

namespace SchoolTimetable
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (TimetableContext _context = new())
            {
                var user = _context.enUsers.FirstOrDefault(x => x.Username == tbUsername.Text);
                if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(tbPassword.Text, user.Password))
                {
                    MessageBox.Show("Helytelen felhasználónév vagy jelszó!", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var m = new MainWindow(tbUsername.Text);
                    this.Hide();
                    m.ShowDialog();
                }
            }
        }
    }
}
