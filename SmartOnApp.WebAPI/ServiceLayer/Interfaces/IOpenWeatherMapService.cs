using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartOnApp.WebAPI.ServiceLayer.Interfaces
{
    public enum Unit
    {
        Metric,
        Imperial,
        Kelvin
    }

    public interface IOpenWeatherMapService
    {
        Task<List<WeatherForecast>> GetFiveDayForecast(string location, Unit unit = Unit.Metric);
    }
}

