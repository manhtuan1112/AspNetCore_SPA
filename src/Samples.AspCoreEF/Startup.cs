using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Samples.AspCoreEF.DAL.EF.EntityFrameworkContext;
using Samples.AspCoreEF.DAL.EF.Infrastructure;
using Samples.AspCoreEF.Service;
using Samples.AspCoreEF.DAL.EF.Repositories;
using AutoMapper;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.Filters;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Samples.AspCoreEF
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(SampleActionFilter)); // by type
                options.Filters.Add(typeof(UnitOfWorkFilter)); // an instance
                options.Filters.Add(typeof(GlobalExceptionFilter));
            });
            services.AddAutoMapper();
            //services.AddSingleton(Configuration);
            //services.AddSingleton<IConfiguration>(Configuration);
            var connectionString = Configuration.GetSection("ConnectionString").ToString();

            services.AddDbContext<TaskSystemDbContext>(

                opts => opts.UseSqlServer(Configuration.GetConnectionString("SampleDemo"))

            );
            //services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"Assets")),
                RequestPath = new PathString("/Assets")
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
