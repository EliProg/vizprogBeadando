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
    /// Interaction logic for BellScheduleEditWindow.xaml
    /// </summary>
    public partial class BellScheduleEditWindow : FluentWindow
    {
        private readonly TimetableContext context;
        private readonly BellSchedule schedule;
        private readonly bool insert;

        public BellScheduleEditWindow(int? id)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            context = new TimetableContext();
            insert = id == null;
            if (id == null)
            {
                titleBar.Title = "Új csengetés";
                schedule = new BellSchedule();
                context.Add(schedule);
            }
            else
            {
                titleBar.Title = "Csengetés módosítása";
                schedule = context.BellSchedules.Find(id);
                tbLessonNum.Text = schedule.LessonNum.ToString();
                tpStart.Value = DateTime.Today + schedule.StartTime;
                tpEnd.Value = DateTime.Today + schedule.EndTime;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            schedule.StartTime = tpStart.Value.Value.TimeOfDay;
            schedule.EndTime = tpEnd.Value.Value.TimeOfDay;
            context.SaveChanges();
            Log.Db(insert ? "Insert" : "Update", schedule);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
