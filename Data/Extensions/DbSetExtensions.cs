using Microsoft.EntityFrameworkCore;

namespace TestAppProject.Data.Extensions
{
    public static class DbSetExtensions
    {
        // метод расширения для удаления всех членов коллекции DbSet
        public static void Clear<T>(this DbSet<T> dbSet) where T: class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
