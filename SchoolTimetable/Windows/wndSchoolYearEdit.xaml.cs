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
    /// Interaction logic for wndSchoolYearEdit.xaml
    /// </summary>
    public partial class wndSchoolYearEdit : FluentWindow
    {
        private readonly TimetableContext _context;
        private readonly enSchoolYear schoolYear;

        public wndSchoolYearEdit(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _context = new TimetableContext();
            if (id == null)
            {
                schoolYear = new enSchoolYear();
                _context.Add(schoolYear);
            }
            else
            {
                schoolYear = _context.enSchoolYears.Find(id);
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
                //MessageBox.Show("A név megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpStart.SelectedDate == null)
            {
                //MessageBox.Show("A név megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpEnd.SelectedDate == null)
            {
                //MessageBox.Show("A név megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            schoolYear.Name = tbName.Text;
            schoolYear.StartDate = dpStart.SelectedDate.Value;
            schoolYear.EndDate = dpEnd.SelectedDate.Value;
            _context.SaveChanges();
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
