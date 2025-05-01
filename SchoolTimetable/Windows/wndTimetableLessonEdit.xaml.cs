using cnTimetable;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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

namespace SchoolTimetable.Windows
{
    /// <summary>
    /// Interaction logic for wndTimetableLessonEdit.xaml
    /// </summary>
    public partial class wndTimetableLessonEdit : Window
    {

        TimetableContext _context;
        string[] napok = { "Hétfő", "Kedd", "Szerda", "Csütörtök", "Péntek"};
        
        enTimetableLesson table;
        public wndTimetableLessonEdit(int id)
        {
            InitializeComponent();

            _context = new TimetableContext();
            if (id != 0)
            {
                table = _context.enTimetableLessons.SingleOrDefault(b => b.Id == id);
            }
            else
            {
                table = new enTimetableLesson();
            }
            Class.ItemsSource = (from c in _context.enClasses
                                 select c.Name).ToList();
            Subject.ItemsSource = (from s in _context.enSubjects
                                   select s.Name).ToList();
            Teacher.ItemsSource = (from t in _context.enUsers
                                   select t.Name).ToList();
            Day.ItemsSource = napok.ToList();
            LessonNum.ItemsSource = (from l in _context.enLessonSchedules
                                     select l.LessonNum).ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            table.SubjectId = (from s in _context.enSubjects
                               where s.Name == Subject.SelectedValue
                               select s.Id).First();

            table.TeacherId = (from s in _context.enUsers
                               where s.Name == Teacher.SelectedValue
                               select s.Id).First();

            table.ClassId = (from s in _context.enClasses
                               where s.Name == Class.SelectedValue
                               select s.Id).First();

            switch (Day.SelectedValue)
            {
                case "Hétfő":
                    table.DayNum = 1;
                    break;
                case "Kedd":
                    table.DayNum = 2;
                    break;
                case "Szerda":
                    table.DayNum = 3;
                    break;
                case "Csütörtök":
                    table.DayNum = 4;
                    break;
                case "Péntek":
                    table.DayNum = 5;
                    break;
                default:
                    table.DayNum = 1;
                    break;
            }
            table.StartDate = Start.DisplayDate;
            table.EndDate = End.DisplayDate;
            table.LessonNum = (from l in _context.enLessonSchedules
                               where l.LessonNum == LessonNum.SelectedIndex + 1
                               select l.LessonNum).First();

            table.SchoolYearId = (from s in _context.enSchoolYears
                                  select s.Id).First();

            if (table.Id == 0)
            {
                
                _context.enTimetableLessons.Add(table);
                
            }
            _context.SaveChanges();
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
