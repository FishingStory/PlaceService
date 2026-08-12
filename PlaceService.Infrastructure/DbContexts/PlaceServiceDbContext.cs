using Microsoft.EntityFrameworkCore;
using PlaceService.Domain.Entities.Models;

namespace PlaceService.Infrastructure.DbContexts;

public class PlaceServiceDbContext(DbContextOptions<PlaceServiceDbContext> options) : DbContext(options)
{
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>()
            .Property(l => l.Coordinates)
            .HasColumnType("geometry(Point,4326)");

        base.OnModelCreating(modelBuilder);
    }
}