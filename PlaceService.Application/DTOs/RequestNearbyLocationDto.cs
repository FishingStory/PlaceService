namespace PlaceService.Application.DTOs;

public class RequestNearbyLocationDto
{
    public string Latitude { get; set; } =  string.Empty;
    
    public string Longitude { get; set; } = String.Empty;
    
    public double Distance { get; set; } = 0.0;
    
}