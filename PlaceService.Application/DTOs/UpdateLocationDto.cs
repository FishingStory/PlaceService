namespace PlaceService.Application.DTOs;

public class UpdateLocationDto
{
    public Guid Id { get; set; }
    
    public String Name { get; set; } = String.Empty;
    
    public string Latitude { get; set; } =  string.Empty;
    
    public string Longitude { get; set; } = String.Empty;
}