
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace PlaceService.Api.Extensions;

public static class AddResilientPipelineExtension
{
    public static IHttpClientBuilder AddPolyPipeline(this IHttpClientBuilder httpClientBuilder)
    {
        httpClientBuilder.AddStandardResilienceHandler(options =>
        {
            options.Retry.MaxRetryAttempts = 2;
            options.Retry.Delay = TimeSpan.FromMilliseconds(250);
            options.Retry.ShouldRetryAfterHeader = true;

            
            options.Retry.DisableForUnsafeHttpMethods();

            options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(3);
            options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(10);

            options.CircuitBreaker.FailureRatio = 0.5;
            options.CircuitBreaker.MinimumThroughput = 10;
            options.CircuitBreaker.SamplingDuration =
                TimeSpan.FromSeconds(30);
            options.CircuitBreaker.BreakDuration =
                TimeSpan.FromSeconds(15);
        });
        return httpClientBuilder;
    }
}