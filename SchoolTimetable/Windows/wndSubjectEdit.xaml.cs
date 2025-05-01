using cnTimetable;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    /// Interaction logic for wndSubjectEdit.xaml
    /// </summary>
    public partial class wndSubjectEdit : Window
    {

        TimetableContext _context;

        enSubject subject;
        public wndSubjectEdit(int id)
        {
            InitializeComponent();
            _context = new TimetableContext();
            if (id != 0)
            {
                subject = _context.enSubjects.SingleOrDefault(b => b.Id == id);
            }
            else
            {
                subject = new enSubject();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            subject.Name = name.Text;
            if (subject.Id == 0)
            {
                _context.enSubjects.Add(subject);
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
