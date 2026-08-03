using PlaceService.Application.DTOs;
using PlaceService.Application.IMappers;
using PlaceService.Domain.Entities.Models;
using NetTopologySuite.Geometries;
using Location = PlaceService.Domain.Entities.Models.Location;

namespace PlaceService.Application.Mappers;


public class LocationMapper(GeometryFactory geometryFactory): ILocationMapper
{
    private readonly GeometryFactory _geometryFactory =  geometryFactory;
    
    public Location MapCreateLocationDtoToLocation(CreateLocationDto location)
    {
        return new Location
        {
            Id = Guid.NewGuid(),
            Name = location.Name,
            Coordinates = _geometryFactory.CreatePoint(new Coordinate(location.Latitude, location.Longitude)),
        };
    }

    public Location MapUpdateLocationDtoToLocation(UpdateLocationDto location)
    {
        return new Location
        {
            Id = location.Id,
            Name = location.Name,
            Coordinates = _geometryFactory.CreatePoint(new Coordinate(location.Latitude, location.Longitude)),
        };
    }

    public ResponseLocationDto MapLocationToResponseLocationDto(Location location)
    {
        return new ResponseLocationDto()
        {
            Id = location.Id,
            Name = location.Name,
            Coordinates = location.Coordinates,
        };
    }
    
    public List<ResponseLocationDto> MapMultipleLocationToResponseLocationDto(List<Location> nearbyLocations)
    {
        var nearbyLocationDtOs = new List<ResponseLocationDto>();

        
        foreach (var nearbyLocation in nearbyLocations) //todo
            nearbyLocationDtOs.Add(MapLocationToResponseLocationDto(nearbyLocation));
        
        return nearbyLocationDtOs;
    }
}