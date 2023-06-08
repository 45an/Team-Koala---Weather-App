namespace WeatherApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
                Wind = 10
            };

            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsJsonAsync(weather);
        });

        app.MapGet("/favorites", (HttpContext httpContext) =>
        {
            var favoriteCity = httpContext.Items["FavoriteCity"] as string;
            if (string.IsNullOrEmpty(favoriteCity))
            {
                httpContext.Response.StatusCode = 404;
                return httpContext.Response.WriteAsync("Favorite city not found");
            }

            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsync(favoriteCity);
        });

        app.MapPost("/favorites", (HttpContext httpContext) =>
        {
            using var reader = new StreamReader(httpContext.Request.Body);
            var city = reader.ReadToEnd();

            httpContext.Items["FavoriteCity"] = city;
            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsync("Favorite city saved");
        });


        app.MapGet("/health", (HttpContext httpContext) =>
        {
            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsync("API is running.");
        });

        app.Run();
    }
}
