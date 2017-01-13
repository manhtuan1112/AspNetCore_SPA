﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.IdentityServer.Configurations;
using IdentityServer4.Validation;
using IdentityServer4.Services;

namespace Sample.IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
          .AddInMemoryApiResources(Configurations.ApiResources.GetApiResources())
          .AddInMemoryClients(Configurations.Clients.GetClients());

            services.AddTransient<IResourceOwnerPasswordValidator, Configurations.ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, Configurations.ProfileService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();

        }
    }
}
