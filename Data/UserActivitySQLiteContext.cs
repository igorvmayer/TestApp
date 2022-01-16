using TestAppProject.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace TestAppProject.Data
{
    public class UserActivitySQLiteContext: DbContext
    {
        public string dbPath { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Filename=MyDatabase.db");

        public DbSet<UserActivity> Users { get; set; }
    }
}
