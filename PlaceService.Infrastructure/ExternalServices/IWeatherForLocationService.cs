using PlaceService.Domain.Entities.TPAResponseModels;
using Refit;

namespace PlaceService.Infrastructure.ExternalServices;

public interface IWeatherForLocationService
{
    [Get("/current.json?q={lat},{lon}")]
    public Task<CurrentWeatherWrapper> GetWeatherForLocation(double lat, double lon);
}