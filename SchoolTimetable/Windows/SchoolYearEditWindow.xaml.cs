using cnTimetable;
using Models;
using SchoolTimetable.Helpers;
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
    /// Interaction logic for SchoolYearEditWindow.xaml
    /// </summary>
    public partial class SchoolYearEditWindow : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly SchoolYear schoolYear;
        private readonly bool insert;

        public SchoolYearEditWindow(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();
            insert = id == null;
            if (id == null)
            {
                titleBar.Title = "Új tanév";
                schoolYear = new SchoolYear();
                context.Add(schoolYear);
                schoolYear.StartDate = DateTime.Today;
                schoolYear.EndDate = DateTime.Today;
            }
            else
            {
                titleBar.Title = "Tanév módosítása";
                schoolYear = context.SchoolYears.Find(id);
            }
            this.DataContext = schoolYear;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(schoolYear.Name))
            {
                await UiMessageBox.Show("A név megadása kötelező!", "Hiba");
                return;
            }
            if (context.SchoolYears.Any(y => y.Id != schoolYear.Id && y.Name == schoolYear.Name))
            {
                await UiMessageBox.Show("A megadott név már foglalt!", "Hiba");
                return;
            }
            if (dpStart.SelectedDate == null)
            {
                await UiMessageBox.Show("A kezdődátum megadása kötelező!", "Hiba");
                return;
            }
            if (dpEnd.SelectedDate == null)
            {
                await UiMessageBox.Show("A végdátum megadása kötelező!", "Hiba");
                return;
            }
            context.SaveChanges();
            Log.Db(insert ? "Insert" : "Update", schoolYear);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
