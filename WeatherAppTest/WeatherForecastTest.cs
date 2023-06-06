using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace WeatherAppTest;


public class WeatherForecastTest
{

    private readonly HttpClient _httpClient = new HttpClient()
    {
        BaseAddress = new Uri("https://dev.kjeld.io:40300")
    };

    private readonly HttpClient _httpClientTwo = new HttpClient()
    {
        BaseAddress = new Uri("https://dev.kjeld.io:20300")
    };


    [Fact]
    public async Task WeatherEndpoint_ReturnsWeatherData()
    {
        // Act
        var response = await _httpClient.GetAsync("/weather/stockholm");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Stockholm", content);
        Assert.Contains("20", content);
        Assert.Contains("70", content);
        Assert.Contains("10", content);
    }

    [Fact]
    public async Task HealthEndpoint_ReturnsOk()
    {
        // Act
        var response = await _httpClientTwo.GetAsync("/health");
        var content = await response.Content.ReadAsStringAsync();

        // Verify the response
        Assert.True(response.IsSuccessStatusCode, "The request was not successful.");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("API is running.", content);
    }

}


