using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Samples.AspCoreEF.DAL.EF.Models;

namespace Samples.AspCoreEF.DAL.EF.EntityFrameworkContext
{
    public class TaskSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        public TaskSystemDbContext(DbContextOptions<TaskSystemDbContext> dbContextOption) : base(dbContextOption)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCategory>().ToTable("ProductCategory");
            builder.Entity<Product>().ToTable("Product");
            builder.Entity<Tag>().ToTable("Tag");
            builder.Entity<ProductTag>().ToTable("ProductTag");
            builder.Entity<ProductTag>().HasKey(t => new { t.ProductID, t.TagID });

            builder.Entity<IdentityUserRole<string>>().HasKey(i => new { i.UserId, i.RoleId });
            builder.Entity<IdentityUserRole<string>>().ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin<string>>().HasKey(i => i.UserId);
            builder.Entity<IdentityUserLogin<string>>().ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().HasKey(i => i.Id);
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUser>().HasKey(i => i.Id);
            builder.Entity<IdentityUser>().ToTable("ApplicationUsers");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("ApplicationRoleClaims");
            builder.Entity<IdentityUserClaim<string>>().HasKey(i => i.UserId);
            builder.Entity<IdentityUserClaim<string>>().ToTable("ApplicationUserClaims");
            builder.Entity<IdentityUserToken<string>>().HasKey(i => i.UserId);
            builder.Entity<IdentityUserToken<string>>().ToTable("ApplicationUserTokens");
        }
    }
}