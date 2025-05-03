using cnTimetable;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTimetable.Helpers
{
    internal static class Log
    {
        public static void Db(string type, object obj)
        {
            var context = new TimetableContext();
            var _event = new Event
            {
                Time = DateTime.Now,
                UserId = Session.user.Id,
                Type = type,
                Param1 = context.Model.FindEntityType(obj.GetType().ToString()).GetTableName(),
                Param2 = obj.GetType().GetProperty("Id").GetValue(obj).ToString()
            };
            context.Add(_event);
            context.SaveChanges();
        }

        public static void Login(string type, User? user, string username)
        {
            var context = new TimetableContext();
            var _event = new Event
            {
                Time = DateTime.Now,
                UserId = user?.Id,
                Type = type + " login"
            };
            if (user == null)
            {
                _event.Param1 = username;
            }
            context.Add(_event);
            context.SaveChanges();
        }
    }
}
