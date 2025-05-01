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

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndClassEdit.xaml
    /// </summary>
    public partial class wndClassEdit : Window
    {

        TimetableContext _context;

        enClass Class;

        public wndClassEdit(int id)
        {
            InitializeComponent();

            _context = new TimetableContext();
            if (id != 0)
            {
                Class = _context.enClasses.SingleOrDefault(b => b.Id == id);
            }
            else
            {
                Class = new enClass();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Class.Name = name.Text;
            if (Class.Id == 0)
            {
                _context.enClasses.Add(Class);
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
