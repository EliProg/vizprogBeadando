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
    /// Interaction logic for wndSchoolYearEdit.xaml
    /// </summary>
    public partial class wndSchoolYearEdit : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly enSchoolYear schoolYear;
        private readonly bool insert;

        public wndSchoolYearEdit(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();
            insert = id == null;
            if (id == null)
            {
                titleBar.Title = "Új tanév";
                schoolYear = new enSchoolYear();
                context.Add(schoolYear);
            }
            else
            {
                titleBar.Title = "Tanév módosítása";
                schoolYear = context.enSchoolYears.Find(id);
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
