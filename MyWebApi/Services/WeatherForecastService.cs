namespace MyWebApi.Services;

public sealed class WeatherForecastService(HttpClient client)
{
    public async Task<WeatherForecastRecord?> GetWeatherForecast()
    {
        var content = await client.GetFromJsonAsync<WeatherForecastRecord>("weatherforecast");

        return content;
    }

}
