namespace PlaceService.Domain.Entities.TPAResponseModels;

public class LocationWeatherDto
{
    public double TemperatureC { get; init; }

    public double WindMph { get; init; }

    public double PressureMb { get; init; }
}