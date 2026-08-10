using Microsoft.Extensions.Options;
using PlaceService.Api.ApiDelegateHandlers;
using PlaceService.Api.Options;
using PlaceService.Infrastructure.ExternalServices;
using Refit;

namespace PlaceService.Api.Extensions;

public static class AddWeatherHttpClintExtension
{
    public static IServiceCollection AddWeatherHttpClient(this IServiceCollection services)
    {
        services.AddTransient<WeatherApiDelegateHandler>();

        services.AddRefitClient<IWeatherForLocationService>()
            .ConfigureHttpClient((provider, client) =>
            {
                var weatherApiOptions = provider.GetRequiredService<IOptions<WeatherForecastOptions>>().Value;
                client.BaseAddress = new Uri($"{weatherApiOptions.BaseUrl}");
            })
            .AddHttpMessageHandler<WeatherApiDelegateHandler>()
            .AddPolyPipeline();

        return services;
    }
}