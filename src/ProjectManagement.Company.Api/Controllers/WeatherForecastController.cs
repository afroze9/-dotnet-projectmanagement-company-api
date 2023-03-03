using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Company.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public WeatherForecastController(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("DiscoveryRandom");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<string> Get()
    {
        try
        {
            var res = await _httpClient.GetStringAsync("http://project-api/WeatherForecast");
            return res;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
