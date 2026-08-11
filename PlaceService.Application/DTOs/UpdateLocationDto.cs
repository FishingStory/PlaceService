namespace PlaceService.Application.DTOs;

public class UpdateLocationDto : LocationDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}