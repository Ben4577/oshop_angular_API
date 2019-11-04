using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.RepositoryFactory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using oshop_angular_API.Services;
using DataAccess.DocumentDb;

namespace oshop_angular_API
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
            var settings = CreateAppSettingsProvider();
            services.AddControllers();
            
            services.AddScoped<IOshopService, OshopService>();
            services.AddSingleton<IRepositoryFactory, RepositoryFactory>(_ => new RepositoryFactory(settings));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static AppRuntimeSettingsProvider CreateAppSettingsProvider()
        {
            var appSettingsProvider = new ConfigFileAppSettingsProvider();
            //appSettingsProvider.Initialise(Configuration);
            return appSettingsProvider;
        }


    }
}
