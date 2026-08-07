using NetTopologySuite.Geometries;

namespace PlaceService.Application.DTOs;

public class ResponseLocationDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public Point Coordinates { get; set; } =  Point.Empty;
    
    public double AvgTemperature { get; set; } = 0.0;
    
    public double AvgPressure { get; set; } = 0.0;
    
    public double AvgWindSpeed { get; set; } = 0.0;
}