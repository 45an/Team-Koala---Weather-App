namespace WeatherApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseAuthorization();
        

        app.MapGet("/weather/stockholm", (HttpContext httpContext) =>
        {
            var weather = new WeatherForecast
            {
                
                City = "Stockholm",
                Temperature = 20,
                Humidity = 70,
                Wind = 10,
                Date = DateTime.Today
            };

            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsJsonAsync(weather);
        });

        app.MapGet("/weather/stockholm", (HttpContext httpContext) =>
        {
            var weather = new WeatherForecast
            {
                City = "Stockholm",
                Temperature = 20,
                Humidity = 70,
                Wind = 10
            };

            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsJsonAsync(weather);
        });

        app.MapGet("/health", (HttpContext httpContext) =>
        {
            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsync("API is running.");
        });

        
        app.Run();
    }
}
