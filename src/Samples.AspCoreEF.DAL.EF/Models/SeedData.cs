using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                var user = new ApplicationUser { UserName = "duongtuan1211@gmail.com", Email = "duongtuan1211@gmail.com" };
                if (context.ApplicationUsers.Count(x=>x.Email==user.Email)==0)
                {
                    userManager.CreateAsync(user, "Admin@123");
                    context.SaveChanges();
                }
                
                // Look for any movies.
                if (!context.Persons.Any())
                {
                    context.Persons.AddRange(
                         new Person
                         {
                             Name = "Tuan",
                             AddedDate = DateTime.Now,
                             ModifiedDate = DateTime.Now
                         },
                         new Person
                         {
                             Name = "Dung",
                             AddedDate = DateTime.Now,
                             ModifiedDate = DateTime.Now
                         }
                     );
                    return;   // DB has been seeded
                }

                
              
                context.SaveChanges();
            }

        }
    }
}
