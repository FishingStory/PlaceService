using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using PlaceService.Api.Options;

namespace PlaceService.Api.Extensions;

public static class AddResilientPipelineExtension
{
    public static IHttpClientBuilder AddPolyPipeline(this IHttpClientBuilder httpClientBuilder)
    {
        var resiliencePipeline = httpClientBuilder.AddStandardResilienceHandler();

        resiliencePipeline.Configure((options, provider) =>
        {
            var resilienceOptions = provider
                .GetRequiredService<IOptions<WeatherForecastResilienceOptions>>()
                .Value;

            options.Retry.MaxRetryAttempts = resilienceOptions.MaxRetryAttempts;
            options.Retry.Delay = resilienceOptions.RetryDelay;
            options.Retry.ShouldRetryAfterHeader = resilienceOptions.ShouldRetryAfterHeader;
            options.Retry.DisableForUnsafeHttpMethods();

            options.AttemptTimeout.Timeout = resilienceOptions.AttemptTimeout;
            options.TotalRequestTimeout.Timeout = resilienceOptions.TotalRequestTimeout;

            options.CircuitBreaker.FailureRatio = resilienceOptions.CircuitBreakerFailureRatio;
            options.CircuitBreaker.MinimumThroughput = resilienceOptions.CircuitBreakerMinimumThroughput;
            options.CircuitBreaker.SamplingDuration = resilienceOptions.CircuitBreakerSamplingDuration;
            options.CircuitBreaker.BreakDuration = resilienceOptions.CircuitBreakerBreakDuration;
        });

        return httpClientBuilder;
    }
}
