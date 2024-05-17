namespace MyWebApi.Endpoints.WeatherForecast;

public class GetWeatherForecast : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastRecord
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
            .WithTags(Tags.WeatherForecast)
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
}
