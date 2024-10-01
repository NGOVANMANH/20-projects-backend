using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;

public class WeatherService
{
    private readonly string _serviceEndpoint;
    private readonly string _serviceQuery;
    private readonly HttpClient _httpClient;

    public WeatherService(IOptions<WeatherService3rd> weatherServiceConfig, HttpClient httpClient)
    {
        _serviceEndpoint = weatherServiceConfig.Value.Endpoint;
        _serviceQuery = weatherServiceConfig.Value.Query;
        _httpClient = httpClient;
    }

    public async Task<JsonObject?> GetWeatherAsync(string location)
    {
        var uri = $"{_serviceEndpoint}/{location}?{_serviceQuery}";

        var response = await _httpClient.GetAsync(uri);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var jsonContent = JsonSerializer.Deserialize<JsonObject>(content);
        return jsonContent;
    }
}