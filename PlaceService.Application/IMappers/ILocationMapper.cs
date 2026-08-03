using PlaceService.Domain.Entities.Models;
using PlaceService.Application.DTOs;

namespace PlaceService.Application.IMappers;

public interface ILocationMapper
{
    public Location MapCreateLocationDtoToLocation(CreateLocationDto location);
    
    public Location MapUpdateLocationDtoToLocation(UpdateLocationDto location);
    
    public ResponseLocationDto MapLocationToResponseLocationDto(Location location);

    public List<ResponseLocationDto> MapMultipleLocationToResponseLocationDto(List<Location> nearbyLocations);

}