using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppProject.Data;
using Microsoft.Extensions.DependencyInjection;

namespace TestAppProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // при необходимости создаем и наполняем тестовую базу данных
            CreateDatabase(host);
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
        private static void CreateDatabase(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                // запрашиваем IoC для получения контекста базы данных для тестового наполнения базы
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<UserActivitySQLiteContext>();
                    TestDBCreator.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<Logger<Program>>();
                    logger.LogError(ex, "Ошибка создания базы данных");
                }

            }
        }
    }
}
