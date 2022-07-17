using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmartOnApp.Shared.DomainLayer.OpenWeatherModels
{
    public class OpenWeatherMapResponse
    {
        [JsonPropertyName("list")]
        public List<Forecast> Forecasts { get; set; }
    }
}

