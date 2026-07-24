using PlaceService.Application.DTOs;
using PlaceService.Application.IMappers;
using PlaceService.Domain.Entities.Models;
using NetTopologySuite.Geometries;
using Location = PlaceService.Domain.Entities.Models.Location;

namespace PlaceService.Application.Mappers;


public class LocationMapper(ILocationMapper locationMapper): ILocationMapper
{
    public Location MapCreateLocationDtoToLocation(CreateLocationDto location)
    {
        return new Location
        {
            Id = Guid.NewGuid(),
            Name = location.Name,
            Coordinates = new Point(location.Latitude, location.Longitude),
            AvgPressure = 0.0,
            AvgTemperature = 0.0
        };
    }

    public Location MapUpdateLocationDtoToLocation(UpdateLocationDto location)
    {
        return new Location
        {
            Id = location.Id,
            Name = location.Name,
            Coordinates = new Point(location.Latitude, location.Longitude),
            AvgPressure = 0.0,
            AvgTemperature = 0.0
        };
    }

    public ResponseLocationDto MapLocationToResponseLocationDto(Location location)
    {
        return new ResponseLocationDto()
        {
            Id = location.Id,
            Name = location.Name,
            Coordinates = location.Coordinates,
            AvgPressure = location.AvgPressure,
            AvgTemperature = location.AvgTemperature
        };
    }
    
}