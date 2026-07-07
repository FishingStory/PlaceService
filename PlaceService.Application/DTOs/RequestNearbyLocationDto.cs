namespace PlaceService.Application.DTOs;

public class RequestNearbyLocationDto : RequestSingleLocationDto
{
    public double Distance { get; set; } = 0.0;
}