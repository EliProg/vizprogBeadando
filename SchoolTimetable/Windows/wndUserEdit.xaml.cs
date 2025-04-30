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
    /// Interaction logic for wndUserEdit.xaml
    /// </summary>
    public partial class wndUserEdit : Window
    {
        private readonly TimetableContext _context;
        private readonly enUser user;

        public wndUserEdit(int id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _context = new TimetableContext();
            user = _context.enUsers.Find(id);
            tbName.Text = user.Name;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            user.Name = tbName.Text;
            _context.SaveChanges();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
