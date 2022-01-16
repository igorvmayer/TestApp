using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestAppProject.Models;

namespace TestAppProject.Services
{
    public interface IUserActivityService
    {
        public Task SaveUserActivities(IEnumerable<UserActivity> items);
        public Task DeleteAllUserActivities();

        // возвращает из хранилища коллекцию UserActivity, которая удовлетворяет условию condition
        public Task<IEnumerable<UserActivity>> GetUserActivities(Expression<Func<UserActivity, bool>> condition);

        public Task<IEnumerable<UserActivity>> GetAllUserActivities();
    }
}
