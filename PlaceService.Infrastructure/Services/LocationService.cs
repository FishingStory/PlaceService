using NetTopologySuite.Geometries;
using PlaceService.Application.DTOs;
using PlaceService.Application.IMappers;
using PlaceService.Application.IServices;
using PlaceService.Domain.IRepositories;

namespace PlaceService.Infrastructure.Services;

public class LocationService(GeometryFactory geometryFactory, ILocationRepository repository, ILocationMapper locationMapper) : ILocationService
{
    
    
    public async Task<List<ResponseLocationDto>> GetNearbyLocations(RequestNearbyLocationDto requestSingleLocationDto)
    {
        var point =  geometryFactory
            .CreatePoint(new Coordinate(requestSingleLocationDto.Latitude, requestSingleLocationDto.Longitude));

        try
        {
            var nearbyLocations = await repository
                .GetNearbyLocations(point, requestSingleLocationDto.Distance);

            // write a request for temperature and pressure

            return locationMapper.MapMultipleLocationToResponseLocationDto(nearbyLocations);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message, e);
        }
    }

    public async Task<ResponseLocationDto> GetLocation(Guid id)
    {
        try
        {
            var location = await repository.GetLocation(id);
            
            // write a request for temperature and pressure
            
            return locationMapper.MapLocationToResponseLocationDto(location);
        }
        catch (ArgumentNullException e)
        {
            throw new ArgumentException(e.Message, e);
        }
    }

    public async Task<ResponseLocationDto> AddLocation(CreateLocationDto location)
    {
        try
        {
            var newLocation = locationMapper.MapCreateLocationDtoToLocation(location);
        
            var addedLocation = await repository.AddLocation(newLocation);
        
            // write a request for temperature and pressure
            
            return locationMapper.MapLocationToResponseLocationDto(addedLocation);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message, e);
        }
        
    }

    public async Task<ResponseLocationDto> UpdateLocation(UpdateLocationDto location)
    {
        try
        {
            var locationToUpdate = locationMapper.MapUpdateLocationDtoToLocation(location);
       
            var updatedLocation = await repository.UpdateLocation(locationToUpdate);
       
            // write a request for temperature and pressure
       
            return locationMapper.MapLocationToResponseLocationDto(updatedLocation);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message, e);
        }
       
    }

    public async Task DeleteLocation(DeleteLocationDto location)
    {
        try
        {
            await repository.DeleteLocation(location.Id);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message, e);
        }    
    }
    
    
}