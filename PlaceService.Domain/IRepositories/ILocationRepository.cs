using System.Drawing;
using NetTopologySuite.Geometries;
using Location = PlaceService.Domain.Entities.Models.Location;
using Point = NetTopologySuite.Geometries.Point;

namespace PlaceService.Domain.IRepositories;

public interface ILocationRepository
{
    public Task<List<Location>> GetNearbyLocations(Point userLocation, double distance);
    
    public Task<Location> GetLocation(Guid id);
    
    public Task<List<Location>> AddLocation(Location location);
    
    public Task<List<Location>> DeleteLocation(Location location);
}