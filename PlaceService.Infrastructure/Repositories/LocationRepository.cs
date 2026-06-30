using PlaceService.Domain.Entities.Models;
using PlaceService.Domain.IRepositories;
using Point = NetTopologySuite.Geometries.Point;

namespace PlaceService.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    public async Task<List<Location>> GetNearbyLocations(Point userLocation, double distance)
    {
        throw new NotImplementedException();
    }

    public async Task<Location> GetLocation(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Location>> AddLocation(Location location)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<Location>> DeleteLocation(Location location)
    {
        throw new NotImplementedException();
    }
}