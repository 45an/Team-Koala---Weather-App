using System;

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

        //services cors
        /*
        builder.Services.AddCors(p => p.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true);
        }));*/


        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("http://localhost:20300",
                                        "http://dev.kjeld.io:20300");
                });
        });
        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();
        app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod()
      .WithOrigins("*"));
        app.UseAuthorization();

        app.MapGet("/weather/stockholm", (HttpContext httpContext) =>
        {
            var weather = new WeatherForecast
            {

                City = "Stockholm",
                Temperature = 22,
                Humidity = 70,
                Wind = 10,
                 
            };
                   
            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsJsonAsync(weather);
        });

        app.MapGet("/time", (HttpContext httpContext) =>
        {
            DateTime now = DateTime.Now;

            var time = new
            {
                Hour = now.Hour,
                Minute = now.Minute,
                Second = now.Second
            };

            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsJsonAsync(time);
        });



        app.MapGet("/health", (HttpContext httpContext) =>
        {
            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsync("API is running.");
        });


      
        app.Run();
    }
}
