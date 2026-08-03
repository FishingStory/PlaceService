using NetTopologySuite.Geometries;
using PlaceService.Application.DTOs;
using PlaceService.Application.IMappers;
using PlaceService.Application.IServices;
using PlaceService.Domain.IRepositories;

namespace PlaceService.Infrastructure.Services;

public class LocationService(GeometryFactory geometryFactory, ILocationRepository repository, ILocationMapper locationMapper) : ILocationService
{
    private readonly GeometryFactory _geometryFactory =  geometryFactory;
    private readonly ILocationRepository _locationRepository = repository;
    private readonly ILocationMapper _locationMapper = locationMapper;
    
    public async Task<List<ResponseLocationDto>> GetNearbyLocations(RequestNearbyLocationDto requestSingleLocationDto)
    {
        var point =  _geometryFactory
            .CreatePoint(new Coordinate(requestSingleLocationDto.Latitude, requestSingleLocationDto.Longitude));

        try
        {
            var nearbyLocations = await _locationRepository
                .GetNearbyLocations(point, requestSingleLocationDto.Distance);

            // write a request for temperature and pressure

            return _locationMapper.MapMultipleLocationToResponseLocationDto(nearbyLocations);
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
            var location = await _locationRepository.GetLocation(id);
            
            // write a request for temperature and pressure
            
            return _locationMapper.MapLocationToResponseLocationDto(location);
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
            var newLocation = _locationMapper.MapCreateLocationDtoToLocation(location);
        
            var addedLocation = await _locationRepository.AddLocation(newLocation);
        
            // write a request for temperature and pressure
            
            return _locationMapper.MapLocationToResponseLocationDto(addedLocation);
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
            var locationToUpdate = _locationMapper.MapUpdateLocationDtoToLocation(location);
       
            var updatedLocation = await _locationRepository.UpdateLocation(locationToUpdate);
       
            // write a request for temperature and pressure
       
            return _locationMapper.MapLocationToResponseLocationDto(updatedLocation);
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
            await _locationRepository.DeleteLocation(location.Id);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message, e);
        }    
    }
    
    
}