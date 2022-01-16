using TestAppProject.Models;
using System.Linq;
using System;

namespace TestAppProject.Data
{
    // Класс обеспечивающий тестовое наполнение базы данных SQLite
    public static class TestDBCreator
    {
        public static void Initialize(UserActivitySQLiteContext context)
        {
            // пересоздаем базу данных
            context.Database.EnsureDeleted();            
            context.Database.EnsureCreated();            

            // если в базе есть данные, то выходим
            if(context.Users.Any())
            {
                return;
            }
            
            var userActivities = new UserActivity[] {
                new UserActivity { DateRegistered = new DateTime(2020, 12, 15), DateLastVisit = new DateTime(2020, 12, 22) },
                new UserActivity { DateRegistered = new DateTime(2020, 12, 15), DateLastVisit = new DateTime(2020, 12, 20) },
                new UserActivity { DateRegistered = new DateTime(2020, 12, 15), DateLastVisit = new DateTime(2020, 12, 26) },
                new UserActivity { DateRegistered = new DateTime(2020, 12, 15), DateLastVisit = new DateTime(2020, 12, 25) },
                new UserActivity { DateRegistered = new DateTime(2020, 12, 15), DateLastVisit = new DateTime(2020, 12, 30) }
            };

            foreach(var userActivity in userActivities)
            {
                context.Users.Add(userActivity);
            }

            context.SaveChanges();

        }
    }
}
