using NetTopologySuite.Geometries;
using PlaceService.Application.DTOs;
using PlaceService.Application.IMappers;
using PlaceService.Application.IServices;
using PlaceService.Domain.IRepositories;

namespace PlaceService.Infrastructure.Services;

public class LocationService(
    GeometryFactory geometryFactory,
    ILocationRepository repository,
    ILocationMapper locationMapper,
    IWeatherService weatherService) : ILocationService
{
    public async Task<List<ResponseLocationDto>> GetNearbyLocations(RequestNearbyLocationDto requestSingleLocationDto)
    {
        var point = geometryFactory
            .CreatePoint(new Coordinate(requestSingleLocationDto.Longitude, requestSingleLocationDto.Latitude));

        try
        {
            var nearbyLocations = await repository
                .GetNearbyLocations(point, requestSingleLocationDto.Distance);

            var responseNearbyLocations = locationMapper
                .MapMultipleLocationToResponseLocationDto(nearbyLocations);

            var filledNearbyLocations = await weatherService
                .GetWeatherForMultipleLocations(responseNearbyLocations);

            return filledNearbyLocations;
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message, e);
        }
    }

    public async Task<ResponseLocationDto> GetLocation(Point coordinates)
    {
        try
        {
            var location = await repository.GetLocation(coordinates);

            var responseLocation = locationMapper
                .MapLocationToResponseLocationDto(location);

            var filledResponseLocation = await weatherService
                .GetWeatherForLocation(responseLocation);

            return filledResponseLocation;
        }
        catch (ArgumentNullException e)
        {
            throw new ArgumentException(e.Message, e);
        }
    }

    public async Task<ResponseLocationDto> GetLocationById(Guid locationId)
    {
        var location = await repository.GetLocationById(locationId);

        var responseLocation = locationMapper
            .MapLocationToResponseLocationDto(location);

        return await weatherService.GetWeatherForLocation(responseLocation);
    }

    public async Task<ResponseLocationDto> AddLocation(CreateLocationDto location)
    {
        try
        {
            var newLocation = locationMapper.MapCreateLocationDtoToLocation(location);

            var addedLocation = await repository.AddLocation(newLocation);

            var responseLocation = locationMapper
                .MapLocationToResponseLocationDto(addedLocation);

            var filledResponseLocation = await weatherService
                .GetWeatherForLocation(responseLocation);

            return filledResponseLocation;
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

            var responseLocation = locationMapper
                .MapLocationToResponseLocationDto(updatedLocation);

            var filledResponseLocation = await weatherService
                .GetWeatherForLocation(responseLocation);


            return filledResponseLocation;
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
