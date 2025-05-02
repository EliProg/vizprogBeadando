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
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : FluentWindow
    {
        public wndMain()
        {
            InitializeComponent();

            miSchoolYear.Header = "Tanév: " + Session.schoolYear.Name;
            miUser.Header = "Bejelentkezett: " + Session.user.Name;
            if (Session.user.Admin)
            {
                miAdmin.Visibility = Visibility.Visible;
                miTimetables.Visibility = Visibility.Visible;
            }
            fmMain.Navigate(new pgTimetable());
        }

        private void miLogout_Click(object sender, RoutedEventArgs e)
        {
            Session.clear();
            var window = new wndLogin();
            window.Show();
            this.Close();
        }

        private void miClose_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void miUsers_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new pgUserList());
        }

        private void miTimetableLessons_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new pgTimetableLessonList());
        }

        private void miClasses_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new pgClassList());
        }

        private void miSubjects_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new pgSubjectList());
        }

        private void miSchoolYears_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new pgSchoolYearList());
        }

        private void miEvents_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new pgEventList());
        }

        private void miTimetables_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new pgTimetableSearch());
        }

        private void miMyTimetable_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new pgTimetable());
        }
    }
}