using cnTimetable;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolTimetable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TimetableContext _context;
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string username)
        {
            InitializeComponent();
            //_context = new TimetableContext();
            //MainFrame.Navigate(new UserListPage());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /* LoginWindow barmilehet = new LoginWindow();
             barmilehet.Show(); */

            var window = new UserEditWindow(2);
            window.Show();
        }
    }
}