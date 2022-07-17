using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartOnApp.Shared.DomainLayer.ConfigurationModels;
using SmartOnApp.Shared.DomainLayer.OpenWeatherModels;
using SmartOnApp.WebAPI.ServiceLayer.Interfaces;

namespace SmartOnApp.WebAPI.ServiceLayer.Services
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private OpenWeatherMapConfig _openWeatherMapConfig;
        private IHttpClientFactory _httpClientFactory;

        public OpenWeatherMapService(
            IOptions<OpenWeatherMapConfig> options,
            IHttpClientFactory httpClientFactory)
        {
            _openWeatherMapConfig = options.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<WeatherForecast>> GetFiveDayForecast(string location, Unit unit = Unit.Metric)
        {
            //string url = $"https://api.openweathermap.org/data/2.5/forecast?q={ location }&appid={ _openWeatherMapConfig.ApiKey }&units={ unit }";
            //var forecasts = new List<WeatherForecast>();

            //using (HttpClient client = new HttpClient())
            //{
            //    var response = client.GetAsync(url).Result;
            //    var json = response.Content.ReadAsStringAsync().Result;
            //    var openWeatherMapResponse = JsonSerializer.Deserialize<OpenWeatherMapResponse>(json);

            //    foreach (var forecast in openWeatherMapResponse.Forecasts)
            //    {
            //        forecasts.Add(new WeatherForecast
            //        {
            //            Date = new DateTime(forecast.Dt),
            //            Temp = forecast.Temps.Temp,
            //            FeelsLike = forecast.Temps.FeelsLike,
            //            TempMin = forecast.Temps.TempMin,
            //            TempMax = forecast.Temps.TempMax,
            //        });
            //    }
            //}

            string url = BuildOpenWeatherUrl("forecast", location, unit);
            var forecasts = new List<WeatherForecast>();

            var client = _httpClientFactory.CreateClient("OpenWeatherClient");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
#pragma warning disable SYSLIB0020 // Type or member is obsolete
                var jsonOpts = new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true };
#pragma warning restore SYSLIB0020 // Type or member is obsolete
                var contentStream = await response.Content.ReadAsStreamAsync();
                var openWeatherResponse = await JsonSerializer.DeserializeAsync<OpenWeatherMapResponse>(contentStream, jsonOpts);
                foreach (var forecast in openWeatherResponse.Forecasts)
                {
                    forecasts.Add(new WeatherForecast
                    {
                        Date = new DateTime(forecast.Dt),
                        Temp = forecast.Temps.Temp,
                        FeelsLike = forecast.Temps.FeelsLike,
                        TempMin = forecast.Temps.TempMin,
                        TempMax = forecast.Temps.TempMax,
                    });
                }

                return forecasts;
            }
            else
            {
                throw new OpenWeatherMapException(response.StatusCode, "Error response from OpenWeatherApi: " + response.ReasonPhrase);
            }
        }

        private string BuildOpenWeatherUrl(string resource, string location, Unit unit)
        {
            return $"https://api.openweathermap.org/data/2.5/{resource}" +
                   $"?appid={_openWeatherMapConfig.ApiKey}" +
                   $"&q={location}" +
                   $"&units={unit}";
        }
    }
}

