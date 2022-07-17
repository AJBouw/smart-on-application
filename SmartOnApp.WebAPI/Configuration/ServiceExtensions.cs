using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartOnApp.Shared.DomainLayer;
using SmartOnApp.Shared.DomainLayer.OpenWeatherModels;
using SmartOnApp.WebAPI.RepositoryLayer;
using SmartOnApp.WebAPI.RepositoryLayer.Interfaces;
using SmartOnApp.WebAPI.RepositoryLayer.Repositories;
using SmartOnApp.WebAPI.ServiceLayer.Interfaces;
using SmartOnApp.WebAPI.ServiceLayer.Services;
using SmartOnApp.WebAPI.UserInterfaceLayer.DataTransferObjectMapper;

namespace SmartOnApp.WebAPI.Configuration
{
    public static class ServiceExtensions
    {
        public static void AddConfigurationAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperInitializer));
        }
        
        public static void AddConfigurationCors(this IServiceCollection services)
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

        public static void AddConfigurationIHttpClientFactory(this IServiceCollection services)
        {
            
            services.AddHttpClient();
            //services.AddHttpClient("simple", client =>
            //{
            //    client.BaseAddress = new Uri("http://192.168.178.40");
            //});
        }

        public static void AddConfigurationSwagger(this IServiceCollection services)
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

        public static void AddConfigurationRepositoryPattern(this IServiceCollection services)
        {
            services.AddTransient<IIoTDeviceRepository, IoTDeviceRepository>();
            services.AddTransient<IMcuRepository, McuRepository>();
            services.AddTransient<ILdrRepository, LdrRepository>();
            services.AddTransient<ILightRepository, LightRepository>();
            services.AddTransient<IPirRepository, PirRepository>();
            services.AddTransient<IServoRepository, ServoRepository>();
        }

        public static void AddConfigurationServices(this IServiceCollection services)
        {
            services.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();
        }

        public static void AddConfigurationUnitOfWorkPattern(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
