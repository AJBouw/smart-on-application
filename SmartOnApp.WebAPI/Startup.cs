using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartOnApp.WebAPI.RepositoryLayer;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;
using SmartOnApp.WebAPI.RepositoryLayer.Repositories;
using SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjectMapper;

namespace SmartOnApp.WebAPI
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
            // Register MariaDb database context
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

            services.AddDbContextPool<SmartOnDbContext>(
                options => options.UseMySql(
                    Configuration.GetConnectionString("MariaDbConnectionString"), serverVersion,
                    options => options.EnableRetryOnFailure()));

            // Register Json serializer
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddControllers();

            // Register AutoMapper
            services.AddAutoMapper(typeof(MapperInitializer));

            // Register Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SmartOn",
                    Version = "v1"
                });
            });

            // Register CORS policy so the API allows requests from JavaScript
            // Any allowed for demo purposes
            services.AddCors(c =>
            {
                // Check this for security
                c.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });


            // Services injected
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMcuRepository, McuRepository>();
            services.AddScoped<ILdrRepository, LdrRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable Swagger
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
