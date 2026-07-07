namespace PlaceService.Application.DTOs;

public class UpdateLocationDto : RequestSingleLocationDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
}