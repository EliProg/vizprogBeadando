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
    /// Interaction logic for SubjectEditWindow.xaml
    /// </summary>
    public partial class SubjectEditWindow : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly Subject subject;
        private readonly bool insert;

        public SubjectEditWindow(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();
            insert = id == null;
            if (id == null)
            {
                titleBar.Title = "Új tantárgy";
                subject = new Subject();
                context.Add(subject);
            }
            else
            {
                titleBar.Title = "Tantárgy módosítása";
                subject = context.Subjects.Find(id);
            }
            this.DataContext = subject;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(subject.Name))
            {
                await UiMessageBox.Show("A név megadása kötelező!", "Hiba");
                return;
            }
            if (context.Subjects.Any(s => s.Id != subject.Id && s.Name == subject.Name))
            {
                await UiMessageBox.Show("A megadott név már foglalt!", "Hiba");
                return;
            }
            context.SaveChanges();
            Log.Db(insert ? "Insert" : "Update", subject);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
