using System;
using System.Text.Json.Serialization;

namespace SmartOnApp.Shared.DomainLayer.OpenWeatherModels
{
    public class Forecast
    {
        [JsonPropertyName("dt")]
        public int Dt { get; set; }
        [JsonPropertyName("main")]
        public Temps Temps { get; set; }
    }
}

