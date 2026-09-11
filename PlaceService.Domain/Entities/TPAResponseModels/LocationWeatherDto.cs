using System.Text.Json.Serialization;

namespace PlaceService.Domain.Entities.TPAResponseModels;

public class LocationWeatherDto
{
    [JsonPropertyName("temp_c")]
    public double TemperatureC { get; init; }

    [JsonPropertyName("wind_mph")]
    public double WindMph { get; init; }

    [JsonPropertyName("pressure_mb")]
    public double PressureMb { get; init; }
}