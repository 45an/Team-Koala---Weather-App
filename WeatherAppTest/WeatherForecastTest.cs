using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace WeatherAppTest;


public class WeatherForecastTest
{

    private readonly HttpClient _httpClient = new HttpClient()
    {
        BaseAddress = new Uri("http://dev.kjeld.io:20300")
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
    public async Task FavoritesEndpoint_PostSavesFavoriteCity()
    {
        // Arrange
        var httpClient = new HttpClient();
        var favoriteCity = "Stockholm";
        var httpContent = new StringContent(favoriteCity, Encoding.UTF8, "text/plain");

        // Act
        var response = await httpClient.PostAsync($"{_httpClient.BaseAddress}/favorites", httpContent);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Favorite city saved", content);     

    }


    [Fact]
    public async Task FavoritesEndpoint_ReturnsNotFound()
    {
        // Arrange
        var client = new HttpClient();

        // Act
        var response = await client.GetAsync($"{_httpClient}/favorites");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("Favorite city not found", content);

    }



    [Fact]
    public async Task HealthEndpoint_ReturnsOk()
    {
        // Act
        var response = await _httpClient.GetAsync("/health");
        var content = await response.Content.ReadAsStringAsync();

        // Verify the response
        Assert.True(response.IsSuccessStatusCode, "The request was not successful.");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("API is running.", content);
    }

}


