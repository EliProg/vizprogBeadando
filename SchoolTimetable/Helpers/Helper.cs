using cnTimetable;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchoolTimetable.Helpers
{
    internal static class Helper
    {
        public static void Log(string type, object obj)
        {
            var context = new TimetableContext();
            var _event = new enEvent
            {
                Time = DateTime.Now,
                UserId = Session.user.Id,
                Type = type,
                Description = ""//JsonSerializer.Serialize(obj)
            };
            context.Add(_event);
            context.SaveChanges();
        }
    }
}
