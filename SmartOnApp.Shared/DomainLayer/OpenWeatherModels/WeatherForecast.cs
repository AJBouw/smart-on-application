using System;

namespace SmartOnApp.WebAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public decimal Temp { get; set; }

        public decimal FeelsLike { get; set; }

        public decimal TempMin { get; set; }

        public decimal TempMax { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
