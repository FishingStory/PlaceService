using PlaceService.Application.DTOs;
using PlaceService.Application.IServices;

namespace PlaceService.Infrastructure.Services;

public class LocationService : ILocationService
{
    public async Task<List<ResponseLocationDto>> GetNearbyLocations(RequestNearbyLocationDto requestSingleLocationDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseLocationDto> GetLocation(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseLocationDto> AddLocation(CreateLocationDto location)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseLocationDto> UpdateLocation(UpdateLocationDto location)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteLocation(Guid id)
    {
        throw new NotImplementedException();
    }
    
}