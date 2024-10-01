using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;
    private readonly IConnectionMultiplexer _redis;

    public WeatherController(WeatherService weatherService, IConnectionMultiplexer redis)
    {
        _weatherService = weatherService;
        _redis = redis;
    }

    [HttpGet("{location}")]
    public async Task<IActionResult> GetWeather(string location)
    {
        var db = _redis.GetDatabase();

        var cachedWeather = await db.StringGetAsync(location);

        if (!cachedWeather.IsNullOrEmpty)
        {
            return Ok(cachedWeather.ToString());
        }

        var weatherData = await _weatherService.GetWeatherAsync(location);

        await db.StringSetAsync(location, weatherData?.ToString(), TimeSpan.FromMinutes(30)); // Optional: set expiration to 30 minutes

        return Ok(weatherData);
    }

}
