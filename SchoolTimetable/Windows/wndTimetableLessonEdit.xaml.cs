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
using Wpf.Ui.Controls;

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndTimetableLessonEdit.xaml
    /// </summary>
    public partial class wndTimetableLessonEdit : FluentWindow
    {
        private readonly TimetableContext _context;
        private readonly enTimetableLesson lesson;

        public wndTimetableLessonEdit()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _context = new TimetableContext();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
