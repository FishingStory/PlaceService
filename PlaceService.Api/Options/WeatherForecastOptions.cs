namespace PlaceService.Api.Options;

public sealed class WeatherForecastOptions
{
    public const string WeatherForecastApiOptionsKey = "WeatherForecast";

    public string? BaseUrl { get; set; }

    public string? ApiKey { get; set; }
}
