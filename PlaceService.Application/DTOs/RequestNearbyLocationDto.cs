namespace PlaceService.Application.DTOs;

public class RequestNearbyLocationDto : LocationDto
{
    public double Distance { get; set; } = 0.0;
}