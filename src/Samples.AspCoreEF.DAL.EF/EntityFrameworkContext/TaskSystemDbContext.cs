using Microsoft.EntityFrameworkCore;
using Samples.AspCoreEF.DAL.EF.Models;

namespace Samples.AspCoreEF.DAL.EF.EntityFrameworkContext
{
    public class TaskSystemDbContext : DbContext
    {
        public TaskSystemDbContext(DbContextOptions<TaskSystemDbContext> dbContextOption) : base(dbContextOption)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCategory>().ToTable("ProductCategory");
            builder.Entity<Product>().ToTable("Product");
        }
    }
}