using Microsoft.AspNetCore.Mvc;
namespace EmployeeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public ActionResult<WeatherForecast> Get([FromQuery] string date, [FromQuery] int temperatureC, [FromQuery] string city)
    {
        if (!DateOnly.TryParse(date, out DateOnly parsedDate))
        {
            return BadRequest("Invalid date format. Please use YYYY-MM-DD format.");
        }
        
        if (temperatureC < -50 || temperatureC > 60)
        {
            return BadRequest("Temperature must be between -50°C and 60°C.");
        }
        
        if (string.IsNullOrWhiteSpace(city))
        {
            return BadRequest("City name cannot be empty.");
        }
        
        return new WeatherForecast
        {
            Date = parsedDate,
            TemperatureC = temperatureC,
            City = city
        };
    }
}
