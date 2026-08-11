using Microsoft.EntityFrameworkCore;
using PlaceService.Domain.Entities.Models;

namespace PlaceService.Infrastructure.DbContexts;

public class PlaceServiceDbContext(DbContextOptions<PlaceServiceDbContext> options) : DbContext(options)
{
    public DbSet<Location> Locations { get; set; }
}