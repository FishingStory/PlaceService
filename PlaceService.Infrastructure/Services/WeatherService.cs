using PlaceService.Application.DTOs;
using PlaceService.Application.IMappers;
using PlaceService.Application.IServices;
using PlaceService.Infrastructure.ExternalServices;

namespace PlaceService.Infrastructure.Services;

public class WeatherService(IWeatherForLocationService weatherForLocationService, ILocationMapper locationMapper) : IWeatherService
{
    public async Task<ResponseLocationDto> GetWeatherForLocation(ResponseLocationDto locationDto)
    {
        var weatherInfo = await weatherForLocationService
                .GetWeatherForLocation(locationDto.Coordinates.Y, locationDto.Coordinates.X);

        if (weatherInfo == null)
            throw new ArgumentException("Bad Coordinates format.");
        
        locationMapper.AddWeatherInfoToResponseLocationDto(locationDto, weatherInfo.LocationWeatherDto);
        
        return locationDto;
    }

    public async Task<List<ResponseLocationDto>> GetWeatherForMultipleLocations(List<ResponseLocationDto> listLocationDto)
    {
        var filledListLocationDto = new List<ResponseLocationDto>();

        foreach (var locationToFill in listLocationDto)
        {
            try
            {
                var filledLocation = await GetWeatherForLocation(locationToFill);
                filledListLocationDto.Add(filledLocation);  
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message, e);
            }
        }
        return filledListLocationDto;
    }
}