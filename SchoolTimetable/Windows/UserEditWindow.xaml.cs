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

namespace SchoolTimetable
{
    /// <summary>
    /// Interaction logic for TeacherEditWindow.xaml
    /// </summary>
    public partial class UserEditWindow : Window
    {
        TimetableContext _context;

        enUser user;

        public UserEditWindow(int id)
        {
            InitializeComponent();
            _context = new TimetableContext();
            user = _context.enUsers.SingleOrDefault(b => b.Id == id);
            name.Text = user.Name;
        }

        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            user.Name = name.Text;
            _context.SaveChanges();
            this.Close();
        }
    }
}
