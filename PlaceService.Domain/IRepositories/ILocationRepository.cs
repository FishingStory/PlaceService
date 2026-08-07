using Location = PlaceService.Domain.Entities.Models.Location;
using Point = NetTopologySuite.Geometries.Point;

namespace PlaceService.Domain.IRepositories;

public interface ILocationRepository
{
    public Task<List<Location>> GetNearbyLocations(Point userLocation, double distance);
    
    public Task<Location> GetLocation(Point coordinates);
    
    public Task<Location> AddLocation(Location location);
    
    public Task DeleteLocation(Guid locationId);
    
    public Task<Location> UpdateLocation(Location location);
}