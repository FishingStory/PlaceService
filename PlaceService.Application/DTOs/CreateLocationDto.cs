namespace PlaceService.Application.DTOs;

public class CreateLocationDto{
    
    public String Name { get; set; } = String.Empty;
    
    public string Latitude { get; set; } =  string.Empty;
    
    public string Longitude { get; set; } = String.Empty;
}