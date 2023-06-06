using System.Net;
using System.Net.Http;

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


