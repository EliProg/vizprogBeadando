using cnTimetable;
using Microsoft.EntityFrameworkCore;
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

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for TeacherEditWindow.xaml
    /// </summary>
    public partial class wndUserEdit : Window
    {
        TimetableContext _context;

        enUser user;

        public wndUserEdit(int id)
        {
            InitializeComponent();
            _context = new TimetableContext();
            if (id != 0)
            {
                user = _context.enUsers.SingleOrDefault(b => b.Id == id);
            }
            else
            {
                user = new enUser();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
                user.Name = name.Text;
                user.Username = userName.Text;
                user.PasswordHash = passwd.Text;
            if (admin.IsChecked == true)
            {
                user.Admin = true;
            }
            else
            {
                user.Admin = false;
            }
            user.SchoolYearsActive = 1;
            if (user.Id == 0)
            {
                _context.enUsers.Add(user);
            }
            _context.SaveChanges();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
