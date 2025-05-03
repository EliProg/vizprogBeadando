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
    /// Interaction logic for EventListPage.xaml
    /// </summary>
    public partial class EventListPage : Page
    {
        private void GetList()
        {
            var context = new TimetableContext();
            var events = context.Events.Include(e => e.User).ToList();
            dgEvents.ItemsSource = events;
        }

        public EventListPage()
        {
            InitializeComponent();
            GetList();
        }
    }
}
