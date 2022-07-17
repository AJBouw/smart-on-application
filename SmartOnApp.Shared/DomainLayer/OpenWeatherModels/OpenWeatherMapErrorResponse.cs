using System;
using System.Text.Json.Serialization;

namespace SmartOnApp.Shared.DomainLayer.OpenWeatherModels
{
    public class OpenWeatherMapErrorResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}

