namespace PlaceService.Application.DTOs;

public class CreateLocationDto : RequestSingleLocationDto
{
    public string Name { get; set; } = string.Empty;
}