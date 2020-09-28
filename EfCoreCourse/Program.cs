using System;
using System.Linq;
using EfCoreCourse.Data;
using Microsoft.EntityFrameworkCore;

namespace EfCoreCourse
{
    class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new ApplicationContext();

            var exists = dbContext.Database.GetPendingMigrations().Any();
            if (exists)
            {
                Console.WriteLine("There are pending Migrations.");
            }
            Console.WriteLine("Hello World!");
        }
    }
}
