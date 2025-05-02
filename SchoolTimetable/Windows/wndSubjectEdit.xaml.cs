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
    /// Interaction logic for wndSubjectEdit.xaml
    /// </summary>
    public partial class wndSubjectEdit : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly enSubject subject;

        public wndSubjectEdit(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();
            if (id == null)
            {
                titleBar.Title = "Új tantárgy";
                subject = new enSubject();
                context.Add(subject);
            }
            else
            {
                titleBar.Title = "Tantárgy módosítása";
                subject = context.enSubjects.Find(id);
                tbName.Text = subject.Name;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("A név megadása kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            subject.Name = tbName.Text;
            context.SaveChanges();
            Helper.Log("Update", subject);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
