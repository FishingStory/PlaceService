using PlaceService.Application.DTOs;

namespace PlaceService.Application.IServices;

public interface IWeatherService
{
    public Task<ResponseLocationDto> GetWeatherForLocation(ResponseLocationDto locationDto);

    public Task<List<ResponseLocationDto>> GetWeatherForMultipleLocations(List<ResponseLocationDto> listLocationDto);
}