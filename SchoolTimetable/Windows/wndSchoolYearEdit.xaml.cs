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

using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndSchoolYearEdit.xaml
    /// </summary>
    public partial class wndSchoolYearEdit : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly enSchoolYear schoolYear;

        public wndSchoolYearEdit(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();
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
                tbName.Text = schoolYear.Name;
                dpStart.SelectedDate = schoolYear.StartDate;
                dpEnd.SelectedDate = schoolYear.EndDate;
                cbActive.IsChecked = schoolYear.Active;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("A név megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpStart.SelectedDate == null)
            {
                MessageBox.Show("A kezdődátum megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpEnd.SelectedDate == null)
            {
                MessageBox.Show("A záródátum megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            schoolYear.Name = tbName.Text;
            schoolYear.StartDate = dpStart.SelectedDate.Value;
            schoolYear.EndDate = dpEnd.SelectedDate.Value;
            schoolYear.Active = cbActive.IsChecked == true;
            context.SaveChanges();
            Helper.Log("Update", schoolYear);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
