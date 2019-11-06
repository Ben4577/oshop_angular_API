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
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using IWebHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace oshop_angular_API
{
    public class Startup
    {

        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IWebHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var appSettingsProvider = new ConfigFileAppSettingsProvider();
            appSettingsProvider.Initialise(Configuration);
            var settings = appSettingsProvider;

            services.AddSingleton(Configuration);

            services.AddControllers();
            
            services.AddScoped<IOshopService, OshopService>();
            services.AddSingleton<IRepositoryFactory, RepositoryFactory>(_ => new RepositoryFactory(settings));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                options => options.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
