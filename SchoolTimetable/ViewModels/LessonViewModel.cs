using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTimetable.ViewModels
{
    public class LessonViewModel
    {
        public int SchoolYearId { get; set; }
        public int? LoggedLessonId { get; set; }
        public string? Topic { get; set; }
        public required string Class { get; set; }
        public int ClassId { get; set; }
        public required string Teacher { get; set; }
        public int TeacherId { get; set; }
        public required string Subject { get; set; }
        public int SubjectId { get; set; }
        public DateTime Date { get; set; }
        public byte LessonNum { get; set; }
        public TimeSpan LessonStart { get; set; }
        public TimeSpan LessonEnd { get; set; }
    }
}
