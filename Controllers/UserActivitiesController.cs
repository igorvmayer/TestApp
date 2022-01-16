using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAppProject.Data;
using TestAppProject.Models;
using TestAppProject.Services;

namespace TestAppProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivitiesController : ControllerBase
    {
        private readonly IUserActivityService userActivityService;

        public UserActivitiesController(IUserActivityService service)
        {
            userActivityService = service;
        }

        // возвращает данные по активностям всех пользователей
        // GET: api/UserActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserActivity>>> GetUsersActivities()
        {
            var items = await userActivityService.GetAllUserActivities();

            if(items == null)
                return NotFound();

            return Ok(items);
        }

        // возвращает рассчитанное значение метрики Rolling Retention 7 day для данных, присутствующих в БД
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserActivity>>> GetUsersMetrics()
        {
            // количество пользователей, вернувшихся в систему в 7-ый день после регистрации или позже
            var itemsReturned = await userActivityService.GetUserActivities(user => user.DateLastVisit >= user.DateRegistered.AddDays(7));
            int usersReturnedCount = itemsReturned.Count();

            // количество пользователей, зарегистрировавшихся в системе 7 дней назад или раньше
            var itemsRegistered = await userActivityService.GetUserActivities(user => DateTime.Today >= user.DateRegistered.AddDays(7));
            int usersRegisteredCount = itemsRegistered.Count();

            double rollingRetention = (double)usersReturnedCount / usersRegisteredCount * 100.0;

            return Ok(rollingRetention);
        }

        // Вызывается при нажатии на кнопку Save в пользовательском интерфейсе приложения
        // POST: api/UserActivities        
        [HttpPost]
        public async Task<IActionResult> SaveUsersActivity([FromBody] IEnumerable<UserActivity> userActivities)
        {
            if (userActivities == null)
                BadRequest();

            // удаляем существующие записи в базе (это следует из тех.задания)
            await userActivityService.DeleteAllUserActivities();

            await userActivityService.SaveUserActivities(userActivities);

            return Ok();
        }

        // DELETE: api/UserActivities/5
        [HttpDelete]
        public async Task<IActionResult> DeleteAllUserActivities()
        {
            await userActivityService.DeleteAllUserActivities();
            return Ok();
        }
        
    }
}
