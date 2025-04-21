using cnTimetable;
using Models;
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

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        int userId;

        public wndMain(enUser user)
        {
            InitializeComponent();
            userId = user.Id;
            miUser.Header = $"Bejelentkezett: {user.Username}";
            if (user.Admin)
            {
                miAdmin.Visibility = Visibility.Visible;
                miTimetables.Visibility = Visibility.Visible;
            }
            fmMain.Navigate(new pgTimetable(userId));
        }

        private void miLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var w = new wndLogin();
            w.Show();
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

        private void miSubjects_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new pgSubjectList());
        }

        private void miSchoolYears_Click(object sender, RoutedEventArgs e)
        {
            //fmMain.Navigate(new pgSubjectList());
        }

        private void miTimetables_Click(object sender, RoutedEventArgs e)
        {
            //fmMain.Navigate(new pgSubjectList());
        }

        private void miMyTimetable_Click(object sender, RoutedEventArgs e)
        {
            fmMain.Navigate(new pgTimetable(userId));
        }
    }
}