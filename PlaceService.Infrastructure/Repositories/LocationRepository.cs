using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PlaceService.Application.DTOs;
using PlaceService.Application.IMappers;
using PlaceService.Domain.IRepositories;
using PlaceService.Infrastructure.DbContexts;
using Location = PlaceService.Domain.Entities.Models.Location;
using Point = NetTopologySuite.Geometries.Point;

namespace PlaceService.Infrastructure.Repositories;

public class LocationRepository(PlaceServiceDbContext context) : ILocationRepository
{
    
    private readonly PlaceServiceDbContext _context = context;
    
    public async Task<List<Location>> GetNearbyLocations(Point userLocation, double distance)
    {
         return await _context.Locations
             .AsNoTracking()
             .Where(l => userLocation.IsWithinDistance(l.Coordinates, distance))
             .OrderBy(l => userLocation.Distance(l.Coordinates))
             .Take(15)
             .ToListAsync();
         
        
    }

    public async Task<Location> GetLocation(Guid id)
    {
        var location = await _context.Locations
            .AsNoTracking()
            .Where(l => l.Id == id)
            .FirstOrDefaultAsync();
        
        return location!;
    }

    public async Task<Location> AddLocation(Location location)
    {
        try
        {
            var potentialDuplicate = await _context.Locations.AsNoTracking()
                .FirstOrDefaultAsync(l => l.Coordinates.EqualsTopologically(location.Coordinates));

            if (potentialDuplicate == null)
            {
                throw new ArgumentException("Such location already exists");
            }
        }
        catch (ArgumentException e)
        {
            throw  new ArgumentException(e.Message, e);
        }
        
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        
        return location;
    }

    public async Task DeleteLocation(Guid locationId)
    {
        try
        {
            var locationToDelete = await _context.Locations.AsNoTracking().FirstOrDefaultAsync(l => l.Id == locationId);
            if (locationToDelete == null)
                throw new ArgumentException("Location not found");
            
            _context.Locations.Remove(locationToDelete);
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message, e);
        }
    }

    public async Task<Location> UpdateLocation(Location location)
    {
        try
        {
            var locationToUpdate = _context.Locations.AsNoTracking().FirstOrDefault(l => l.Id == location.Id);
            if (locationToUpdate == null)
            {
                throw new ArgumentException("Location not found");
            }
            _context.Remove(locationToUpdate);
            await _context.SaveChangesAsync();
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message, e);
        }
        
        var updatedLocation = await AddLocation(location);
        await _context.SaveChangesAsync();

        return updatedLocation;
    }
    
}