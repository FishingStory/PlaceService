using Microsoft.EntityFrameworkCore;
using PlaceService.Domain.IRepositories;
using PlaceService.Infrastructure.DbContexts;
using Location = PlaceService.Domain.Entities.Models.Location;
using Point = NetTopologySuite.Geometries.Point;

namespace PlaceService.Infrastructure.Repositories;

public class LocationRepository(PlaceServiceDbContext context) : ILocationRepository
{
    private const double MetersPerDegree = 111_320d;
    
    private const double CoordinateMatchToleranceDegrees = 0.5 / 1000d;

    public async Task<List<Location>> GetNearbyLocations(Point userLocation, double distance)
    {
        var distanceInDegrees = distance / MetersPerDegree;

        var nearbyLocations = await context
            .Locations.AsNoTracking()
            .Where(l => userLocation.IsWithinDistance(l.Coordinates, distanceInDegrees))
            .OrderBy(l => userLocation.Distance(l.Coordinates))
            .Take(15)
            .ToListAsync();

        if (nearbyLocations == null || nearbyLocations.Count == 0)
            throw new ArgumentException("No locations found");

        return nearbyLocations;
    }

    public async Task<Location> GetLocation(Point coordinates)
    {
        var location = await context
            .Locations.AsNoTracking()
            .Where(l => coordinates.IsWithinDistance(l.Coordinates, CoordinateMatchToleranceDegrees))
            .FirstOrDefaultAsync();

        if (location == null) throw new ArgumentException("Location not found");
        return location;
    }

    public async Task<Location> GetLocationById(Guid locationId)
    {
        var location = await context
            .Locations.AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == locationId);

        if (location == null)
            throw new ArgumentException("Location not found");

        return location;
    }

    public async Task<Location> AddLocation(Location location)
    {
        var potentialDuplicate = await context
            .Locations.AsNoTracking()
            .FirstOrDefaultAsync(l => location.Coordinates.IsWithinDistance(
                l.Coordinates,
                CoordinateMatchToleranceDegrees));

        if (potentialDuplicate != null)
            throw new ArgumentException("Such location already exists");

        await context.Locations.AddAsync(location);
        await context.SaveChangesAsync();

        return location;
    }

    public async Task DeleteLocation(Guid locationId)
    {
        var locationToDelete = await context
            .Locations.AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == locationId);

        if (locationToDelete == null)
            throw new ArgumentException("Location not found");

        context.Locations.Remove(locationToDelete);
        await context.SaveChangesAsync();
    }

    public async Task<Location> UpdateLocation(Location location)
    {
        var locationToUpdate = await context
            .Locations.AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == location.Id);

        if (locationToUpdate == null)
            throw new ArgumentException("Location not found");

        await DeleteLocation(locationToUpdate.Id);
        await context.SaveChangesAsync();

        var updatedLocation = await AddLocation(location);
        await context.SaveChangesAsync();

        return updatedLocation;
    }
}
