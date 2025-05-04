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
    /// Interaction logic for ClassEditWindow.xaml
    /// </summary>
    public partial class ClassEditWindow : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly Class _class;
        private readonly bool insert;

        public ClassEditWindow(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();
            insert = id == null;
            if (id == null)
            {
                titleBar.Title = "Új osztály";
                _class = new Class();
                context.Add(_class);
            }
            else
            {
                titleBar.Title = "Osztály módosítása";
                _class = context.Classes.Find(id);
            }
            this.DataContext = _class;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_class.Name))
            {
                await UiMessageBox.Show("A név megadása kötelező!", "Hiba");
                return;
            }
            if (context.Classes.Any(c => c.Id != _class.Id && c.Name == _class.Name))
            {
                await UiMessageBox.Show("A megadott név már foglalt!", "Hiba");
                return;
            }
            context.SaveChanges();
            Log.Db(insert ? "Insert" : "Update", _class);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
