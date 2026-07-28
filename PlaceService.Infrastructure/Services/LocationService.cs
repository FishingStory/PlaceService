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

        var nearbyLocations = await _locationRepository
            .GetNearbyLocations(point, requestSingleLocationDto.Distance);
        
        // write a request for temperature and pressure
        var nearbyLocationDtOs = new List<ResponseLocationDto>();
        foreach (var nearbyLocation in nearbyLocations)
            nearbyLocationDtOs.Add(_locationMapper.MapLocationToResponseLocationDto(nearbyLocation));
        return nearbyLocationDtOs;
    }

    public async Task<ResponseLocationDto> GetLocation(Guid id)
    {
        var location = await _locationRepository.GetLocation(id);

        // write a request for temperature and pressure
        
        return _locationMapper.MapLocationToResponseLocationDto(location);
    }

    public async Task<ResponseLocationDto> AddLocation(CreateLocationDto location)
    {
        var newLocation = _locationMapper.MapCreateLocationDtoToLocation(location);
        
        var addedLocation = await _locationRepository.AddLocation(newLocation);
        
        // write a request for temperature and pressure
        
        return _locationMapper.MapLocationToResponseLocationDto(addedLocation);
    }

    public async Task<ResponseLocationDto> UpdateLocation(UpdateLocationDto location)
    {
       var locationToUpdate = _locationMapper.MapUpdateLocationDtoToLocation(location);
       
       var updatedLocation = await _locationRepository.UpdateLocation(locationToUpdate);
       
       // write a request for temperature and pressure
       
       return _locationMapper.MapLocationToResponseLocationDto(updatedLocation);
    }

    public async Task DeleteLocation(DeleteLocationDto location)
    {
        await _locationRepository.DeleteLocation(location.Id);
    }
}