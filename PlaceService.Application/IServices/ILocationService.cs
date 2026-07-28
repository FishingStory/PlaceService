using PlaceService.Application.DTOs;

namespace PlaceService.Application.IServices;

public interface ILocationService
{
    
    public Task<List<ResponseLocationDto>> GetNearbyLocations(RequestNearbyLocationDto requestSingleLocationDto);
    
    public Task<ResponseLocationDto> GetLocation(Guid id);
    
    public Task<ResponseLocationDto> AddLocation(CreateLocationDto location);
    
    public Task<ResponseLocationDto> UpdateLocation(UpdateLocationDto location);
    
    public Task DeleteLocation(Guid id);
}