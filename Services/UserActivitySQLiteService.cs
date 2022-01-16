using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TestAppProject.Models;
using TestAppProject.Data;
using TestAppProject.Data.Extensions;

namespace TestAppProject.Services
{
    // конкретная реализация сервиса IUserActivityService для работы с SQLite
    public class UserActivitySQLiteService : IUserActivityService
    {
        private UserActivitySQLiteContext DbContext { get; }
        public UserActivitySQLiteService(UserActivitySQLiteContext context)
        {
            DbContext = context;
        }

        public async Task SaveUserActivities(IEnumerable<UserActivity> items)
        {
            foreach(var item in items)
            {
                DbContext.Users.Add(item); 
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAllUserActivities()
        {
            // метод расширения Clear описан в Models.Extensions
            DbContext.Users.Clear();
            await DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserActivity>> GetUserActivities(Expression<Func<UserActivity, bool>> condition)
        {
            return await DbContext.Users.Where(condition).ToListAsync();
        }

        public async Task<IEnumerable<UserActivity>> GetAllUserActivities()
        {
            return await (from user in DbContext.Users select user).ToListAsync();
        }
    }
}
