using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartOnApp.Shared.DomainLayer.ConfigurationModels;
using SmartOnApp.WebAPI.ServiceLayer.Interfaces;

namespace SmartOnApp.WebAPI.UserInterfaceLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenWeatherMapController : ControllerBase
    {
        private readonly ILogger<OpenWeatherMapController> _logger;
        private readonly IOpenWeatherMapService _openWeatherMapService;

        public OpenWeatherMapController(ILogger<OpenWeatherMapController> logger, IOpenWeatherMapService openWeatherMapService)
        {
            _logger = logger;
            _openWeatherMapService = openWeatherMapService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string location, Unit unit = Unit.Imperial)
        {
            if (string.IsNullOrEmpty(location))
            {
                return BadRequest("Location parameter is missing");
            }

            try
            {
                var forecast = await _openWeatherMapService.GetFiveDayForecast(location, unit);

                return Ok(forecast);
            }
            catch (OpenWeatherMapException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return BadRequest($"Location: \"{ location }\" not found.");
                else
                    return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Get)}");
                return StatusCode(500, "Internal server error. Please, try again later");
                //return StatusCode(500, ex.Message);
            }
        }
    }
}

