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
    /// Interaction logic for wndClassEdit.xaml
    /// </summary>
    public partial class wndClassEdit : FluentWindow
    {
        private readonly TimetableContext _context;
        private readonly enClass _class;

        public wndClassEdit(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            _context = new TimetableContext();
            if (id == null)
            {
                _class = new enClass();
                _context.Add(_class);
            }
            else
            {
                _class = _context.enClasses.Find(id);
                tbName.Text = _class.Name;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                //MessageBox.Show("A név megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _class.Name = tbName.Text;
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
