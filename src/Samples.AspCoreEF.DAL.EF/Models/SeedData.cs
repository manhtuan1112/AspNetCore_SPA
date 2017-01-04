using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.DAL.EF.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TaskSystemDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<TaskSystemDbContext>>()))
            {
                // Look for any movies.
                if (context.Persons.Any())
                {
                    return;   // DB has been seeded
                }

                context.Persons.AddRange(
                     new Person
                     {
                         Name = "Tuan",
                         AddedDate = DateTime.Now,
                         ModifiedDate=DateTime.Now
                     },
                     new Person
                     {
                         Name = "Dung",
                         AddedDate = DateTime.Now,
                         ModifiedDate = DateTime.Now
                     }
                );
                context.SaveChanges();
            }

        }
    }
}
