using PlaceService.Application.DTOs;
using PlaceService.Domain.Entities.Models;
using PlaceService.Domain.Entities.TPAResponseModels;

namespace PlaceService.Application.IMappers;

public interface ILocationMapper
{
    public Location MapCreateLocationDtoToLocation(CreateLocationDto location);

    public Location MapUpdateLocationDtoToLocation(UpdateLocationDto location);

    public ResponseLocationDto MapLocationToResponseLocationDto(Location location);

    public List<ResponseLocationDto> MapMultipleLocationToResponseLocationDto(List<Location> nearbyLocations);

    public ResponseLocationDto AddWeatherInfoToResponseLocationDto(ResponseLocationDto location,
        LocationWeatherDto weatherInfo);
}