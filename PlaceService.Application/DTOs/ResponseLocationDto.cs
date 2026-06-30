using NetTopologySuite.Geometries;

namespace PlaceService.Application.DTOs;

public class ResponseLocationDto
{
    public Guid Id { get; set; }
    
    public String Name { get; set; } = string.Empty;
    
    public Point? Coordinates { get; set; } =  null;
    
    public Double AvgTemperature { get; set; } = 0.0;
    
    public Double AvgPressure { get; set; } = 0.0;
}