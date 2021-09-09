using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIOGames.Services;
using DIOGames.Repository;
using DIOGames.Middleware;
using System.Reflection;

namespace DIOGames
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Services
            // Add all services in container app
            var appServices = from service in Assembly.GetExecutingAssembly().GetTypes()
                               where service.IsClass && service.Name.Contains("Service") && service.Namespace.Equals("DIOGames.Services")
                               select service;

            appServices.ToList().ForEach(s => services.AddScoped(s.GetInterfaces().Where(i => i.Name.Contains(s.Name)).Single(), s));

            #endregion

            #region Repository
            // Add all repositories in container app
            var appRepositories = from repository in Assembly.GetExecutingAssembly().GetTypes()
                                  where repository.IsClass && repository.Name.Contains("Repository") && repository.Namespace.Equals("DIOGames.Repository")
                                  select repository;

            appRepositories.ToList().ForEach(r => services.AddScoped(r.GetInterfaces().Where(i => i.Name.Contains(r.Name)).Single(), r));

            #endregion


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DIOGames", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DIOGames v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
