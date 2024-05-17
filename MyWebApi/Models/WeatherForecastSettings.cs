namespace MyWebApi.Models;

public sealed class WeatherForecastSettings
{
    public const string ConfigurationSection = "WeatherForecast";

    [Required, Url]
    public string BaseAddress { get; set; } = string.Empty;

    [Required]
    public string AccessToken { get; set; } = string.Empty;
}
