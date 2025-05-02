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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolTimetable.Pages
{
    /// <summary>
    /// Interaction logic for pgEventList.xaml
    /// </summary>
    public partial class pgEventList : Page
    {
        private void getList()
        {
            var context = new TimetableContext();
            var events = context.enEvents.Include(e => e.enUser).ToList();
            dgEvents.ItemsSource = events;
        }

        public pgEventList()
        {
            InitializeComponent();
            getList();
        }
    }
}
