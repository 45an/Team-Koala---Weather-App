using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;


namespace WeatherAppTest;
internal record Weather(string City , int Temperature, int Humidity, int Wind);
/*
 * 	"date": "2023-06-13T00:00:00+02:00",
	"city": "Stockholm",
	"temperature": 20,
	"humidity": 70,
	"wind": 10,
	"summary": null
 */
public class WeatherForecastTest
{

    private readonly HttpClient _httpClient = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:20300")
    };



    [Fact]
    public async Task WeatherEndpoint_ReturnsWeatherData()
    {
        // Arrange.
        var expectedStatusCode = System.Net.HttpStatusCode.OK;
        var expectedContent = new Weather("Stockholm", 22, 70,10);
        var stopwatch = Stopwatch.StartNew();

        // Act.
        var response = await _httpClient.GetAsync("/weather/stockholm");

        // Assert.
        await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
        // Act
        
        var content = await response.Content.ReadAsStringAsync();

       /* // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Stockholm", content);
        Assert.Contains("20", content);
        Assert.Contains("70", content);
        Assert.Contains("10", content);       */
    }

    

    // Update the test code
    [Fact]
    public async Task TimeEndpoint_ReturnsCurrentTime()
    {
        // Act
        var response = await _httpClient.GetAsync("/time");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        TimeResponse time;
        try
        {
            time = JsonConvert.DeserializeObject<TimeResponse>(content);
        }
        catch (JsonException ex)
        {
            // Handle JSON deserialization error
            Assert.True(false, $"Error deserializing JSON response: {ex.Message}");
            return;
        }

        var currentHour = DateTime.Now.Hour;
        var currentMinute = DateTime.Now.Minute;
        var currentSecond = DateTime.Now.Second;

        Assert.Equal(currentHour, time.Hour);
        Assert.Equal(currentMinute, time.Minute);
        Assert.Equal(currentSecond, time.Second);
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


