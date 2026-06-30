using System.Drawing;

namespace PlaceService.Domain.Entities.Models;

public class Location
{
    public Guid Id { get; set; }
    
    public String Name { get; set; } = String.Empty;
    
    public Point Coordinates { get; set; } =  new Point();
    
    public Double AvgTemperature { get; set; } = 0.0;
    
    public Double AvgPressure { get; set; } = 0.0;
}