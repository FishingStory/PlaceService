using PlaceService.Application.DTOs;
using Refit;

namespace PlaceService.Infrastructure.ExternalServices;

public interface IWeatherForLocationService
{
    [Get("/current.json?q={lat},{lon}")]
    public Task<ResponseLocationDto> GetWeatherForLocation([Query] string lat, [Query] string lon);

}