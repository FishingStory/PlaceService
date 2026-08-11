namespace PlaceService.Api.Options;

public sealed class WeatherForecastResilienceOptions
{
    public const string WeatherForecastResilienceOptionsKey =
        $"{WeatherForecastOptions.WeatherForecastApiOptionsKey}:Resilience";

    public int MaxRetryAttempts { get; set; }

    public TimeSpan RetryDelay { get; set; }

    public bool ShouldRetryAfterHeader { get; set; }

    public TimeSpan AttemptTimeout { get; set; }

    public TimeSpan TotalRequestTimeout { get; set; }

    public double CircuitBreakerFailureRatio { get; set; }

    public int CircuitBreakerMinimumThroughput { get; set; }

    public TimeSpan CircuitBreakerSamplingDuration { get; set; }

    public TimeSpan CircuitBreakerBreakDuration { get; set; }
}
