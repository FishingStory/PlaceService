using Microsoft.Extensions.Options;
using PlaceService.Api.Options;

namespace PlaceService.Api.ApiDelegateHandlers;

public class WeatherApiDelegateHandler(IOptions<WeatherForecastOptions> weatherForecastOptions) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var uri = request.RequestUri!.ToString();
        var separator = uri.Contains('?') ? '&' : '?';

        request.RequestUri = new Uri($"{uri}{separator}key={weatherForecastOptions.Value.ApiKey}");

        return base.SendAsync(request, cancellationToken);
    }
}