using SchoolTimetable.Pages;
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
using Wpf.Ui.Controls;

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            miSchoolYear.Header = "Tanév: " + Session.schoolYear.Name;
            miUser.Header = "Bejelentkezett: " + Session.user.Name;
            if (Session.user.Admin)
            {
                miAdmin.Visibility = Visibility.Visible;
                miTimetables.Visibility = Visibility.Visible;
            }
            fmMain.Navigate(new TimetablePage());
        }

        private void miLogout_Click(object sender, RoutedEventArgs e)
        {
            Session.Clear();
            var window = new LoginWindow();
            window.Show();
            this.Close();
        }

        private void miClose_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void miUsers_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new UserListPage());
        }

        private void miTimetableLessons_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new TimetableLessonListPage());
        }

        private void miBellSchedules_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new BellScheduleListPage());
        }

        private void miClasses_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new ClassListPage());
        }

        private void miSubjects_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new SubjectListPage());
        }

        private void miSchoolYears_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new SchoolYearListPage());
        }

        private void miEvents_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new EventListPage());
        }

        private void miTimetables_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new TimetableSearchPage());
        }

        private void miMyTimetable_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new TimetablePage());
        }
    }
}