using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationAPI.Services;
using Shared;

namespace NotificationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly INotificationService _service;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, INotificationService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("{code}")]
        public async Task<IActionResult> GetWeatherByCode(string code)
        {
            var rng = new Random();
            var weatherModel =  new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Code = code
            };

            await _service.SendMessageToBus(weatherModel);
            return Ok(weatherModel);
        }
    }
}
