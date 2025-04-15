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
using System.Windows.Navigation;
using System.Windows.Shapes;
using cnTimetable;
using Models;

namespace Viz_Projekt_Feladat
{
    /// <summary>
    /// Interaction logic for UserListPage.xaml
    /// </summary>
    public partial class UserListPage : Page
    {
        TimetableContext _context;

        public UserListPage()
        {
            InitializeComponent();
            _context = new TimetableContext();
            var lekerdezes = (from s in _context.enUsers
                              select new enUser
                              {
                                  Id = s.Id,
                                  Username = s.Username,
                                  EduId = s.EduId,
                                  Name = s.Name,
                                  Admin = s.Admin
                              }).ToList();
            dgUsers.ItemsSource = lekerdezes;
        }
    }
}
