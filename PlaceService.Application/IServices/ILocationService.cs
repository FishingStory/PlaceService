using PlaceService.Application.DTOs;

namespace PlaceService.Application.IServices;

public interface ILocationService
{
    
    public Task<List<ResponseLocationDto>> GetNearbyLocations(RequestNearbyLocationDto requestSingleLocationDto);
    
    public Task<ResponseLocationDto> GetLocation(Guid id);
    
    public Task<List<ResponseLocationDto>> AddLocation(RequestSingleLocationDto location);
    
    public Task<List<ResponseLocationDto>> UpdateLocation(RequestSingleLocationDto location);
    
    public Task<List<ResponseLocationDto>> DeleteLocation(RequestSingleLocationDto location);
}