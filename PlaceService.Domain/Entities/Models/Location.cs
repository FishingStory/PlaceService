using NetTopologySuite.Geometries;

namespace PlaceService.Domain.Entities.Models;

public class Location
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Point Coordinates { get; set; } = Point.Empty;
}