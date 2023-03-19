using Microsoft.AspNetCore.Mvc;
using optionator.core;
using optionator.data;

namespace optionator.webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastFactory _weatherForecastFactory;

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        WeatherForecastFactory weatherForecastFactory)
    {
        _logger = logger;
        _weatherForecastFactory = weatherForecastFactory;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        string random = Math.PI.ToString().Replace(".","");
        return Enumerable
            .Range(1, 5)
            .Select(index => {
                int pit = int.Parse(random.Skip(index).First().ToString());
                int temp = 10 + pit;
                DateOnly date = DateOnly.FromDateTime(DateTime.Now.AddDays(index));
                return _weatherForecastFactory.CreateForecast(date, temp);
            })
            .ToArray();
    }
}
