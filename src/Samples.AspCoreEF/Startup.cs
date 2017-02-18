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
using React.AspNet;
using Webpack;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Samples.AspCoreEF.Models;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.Services;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Mvc;
using Samples.AspCoreEF.Infrastructure.Services;

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddWebpack();

            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(SampleActionFilter)); // by type
                options.Filters.Add(typeof(UnitOfWorkFilter)); // an instance
                options.Filters.Add(typeof(GlobalExceptionFilter));
            });
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new RequireHttpsAttribute());
            //});



            services.AddCors(
           options => options.AddPolicy("AllowCors",
               builder => {
                   builder
                       //.WithOrigins("http://localhost:4456") //AllowSpecificOrigins;  
                       //.WithOrigins("http://localhost:4456", "http://localhost:4457") //AllowMultipleOrigins;  
                       .AllowAnyOrigin() //AllowAllOrigins;  

                   //.WithMethods("GET") //AllowSpecificMethods;  
                   //.WithMethods("GET", "PUT") //AllowSpecificMethods;  
                   //.WithMethods("GET", "PUT", "POST") //AllowSpecificMethods;  
                   .WithMethods("GET", "PUT", "POST", "DELETE") //AllowSpecificMethods;  
                                                                //.AllowAnyMethod() //AllowAllMethods;  

                   //.WithHeaders("Accept", "Content-type", "Origin", "X-Custom-Header"); //AllowSpecificHeaders;  
                   .AllowAnyHeader(); //AllowAllHeaders;  
               })
             );

            services.AddAutoMapper();
            //services.AddSingleton(Configuration);
            //services.AddSingleton<IConfiguration>(Configuration);
            var connectionString = Configuration.GetSection("ConnectionString").ToString();

            var appSettings = Configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettings);

            services.AddDbContext<TaskSystemDbContext>(

                opts => opts.UseSqlServer(Configuration.GetConnectionString("SampleDemo"))

            );

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<TaskSystemDbContext>()
            .AddDefaultTokenProviders();

            //services.AddTransient<IProfileService, IdentityWithAdditionalClaimsProfileService>();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);


            // Adds IdentityServer
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>();
            //.AddProfileService<IdentityWithAdditionalClaimsProfileService>();




            //services.AddAuthorization(options => {
            //    options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
            //});

            //services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            //Repositories
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IProductTagRepository, ProductTagRepository>();
            
            services.AddTransient<IApplicationGroupRepository, ApplicationGroupRepository>();
            services.AddTransient<IApplicationRoleRepository, ApplicationRoleRepository>();
            services.AddTransient<IApplicationRoleGroupRepository, ApplicationRoleGroupRepository>();
            services.AddTransient<IApplicationUserGroupRepository, ApplicationUserGroupRepository>();
            //Services
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<IApplicationGroupService, ApplicationGroupService>();
            services.AddTransient<IApplicationRoleService, ApplicationRoleService>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";

                // User settings
                options.User.RequireUniqueEmail = true;
            });
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

            app.UseIdentity();
            app.UseIdentityServer();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:5000",
                RequireHttpsMetadata = false,

                ApiName = "api1"
            });

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //IdentityServerAuthenticationOptions identityServerValidationOptions = new IdentityServerAuthenticationOptions
            //{
            //    Authority = ConfigIdentityServer4.HOST_URL + "/",
            //    AllowedScopes = new List<string> { "dataEventRecords" },
            //    ApiSecret = "dataEventRecordsSecret",
            //    ApiName = "dataEventRecords",
            //    AutomaticAuthenticate = true,
            //    SupportedTokens = SupportedTokens.Both,
            //    // TokenRetriever = _tokenRetriever,
            //    // required if you want to return a 403 and not a 401 for forbidden responses
            //    AutomaticChallenge = true,
            //};

            //app.UseIdentityServerAuthentication(identityServerValidationOptions);


            //Enable CORS policy "AllowCors"  
            app.UseCors("AllowCors");

            SeedData.Initialize(app.ApplicationServices);

        }
    }
}
