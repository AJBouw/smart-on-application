using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SmartOnApp.Shared.DomainLayer;
using SmartOnApp.WebAPI.RepositoryLayer;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;
using SmartOnApp.WebAPI.RepositoryLayer.Repositories;
using SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjectMapper;

namespace SmartOnApp.WebAPI.Configuration
{
    public static class ServiceExtensions
    {
        public static void AddConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperInitializer));
        }
        
        public static void AddConfigureCors(this IServiceCollection services)
        {
            // Register CORS policy so the API allows requests from JavaScript
            // Any allowed for demo purposes
            services.AddCors(options =>
            {
                // Check this for security
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });
        }

        public static void AddConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SmartOn",
                    Version = "v1"
                });
            });
        }

        public static void AddRepositoryPattern(this IServiceCollection services)
        {
            services.AddScoped<IIoTDeviceRepository, IoTDeviceRepository>();
            services.AddScoped<IMcuRepository, McuRepository>();
            services.AddScoped<ILdrRepository, LdrRepository>();
            services.AddScoped<ILightRepository, LightRepository>();
            services.AddScoped<IPirRepository, PirRepository>();
            services.AddScoped<IServoRepository, ServoRepository>();
        }

        public static void AddUnitOfWorkPattern(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
