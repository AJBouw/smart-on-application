using System;
using System.Net;

namespace SmartOnApp.Shared.DomainLayer.ConfigurationModels
{
    public class OpenWeatherMapException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public OpenWeatherMapException() { }

        public OpenWeatherMapException(HttpStatusCode statusCode)
            => StatusCode = statusCode;

        public OpenWeatherMapException(HttpStatusCode statusCode, string message) : base(message)
            => StatusCode = statusCode;

        public OpenWeatherMapException(HttpStatusCode statusCode, string message, Exception inner) : base(message, inner)
            => StatusCode = statusCode;
    }
}

