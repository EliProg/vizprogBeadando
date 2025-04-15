using cnTimetable;
using Microsoft.EntityFrameworkCore;
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

namespace Viz_Projekt_Feladat
{
    /// <summary>
    /// Interaction logic for TeacherEditWindow.xaml
    /// </summary>
    public partial class UserEditWindow : Window
    {
        TimetableContext _context;

        public UserEditWindow(int id)
        {
            InitializeComponent();
            _context = new TimetableContext();
            var lekerdezes = (from s in _context.enUsers
                              where s.Id == id
                              select s.Name).ToList();
            name.Text = lekerdezes[0];
        }
    }
}
