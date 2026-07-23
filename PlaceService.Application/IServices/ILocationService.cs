using PlaceService.Application.DTOs;

namespace PlaceService.Application.IServices;

public interface ILocationService
{
    
    public Task<List<ResponseLocationDto>> GetNearbyLocations(RequestNearbyLocationDto requestSingleLocationDto);
    
    public Task<ResponseLocationDto> GetLocation(Guid id);
    
    public Task<List<ResponseLocationDto>> AddLocation(LocationDto location);
    
    public Task<List<ResponseLocationDto>> UpdateLocation(LocationDto location);
    
    public Task<List<ResponseLocationDto>> DeleteLocation(LocationDto location);
}