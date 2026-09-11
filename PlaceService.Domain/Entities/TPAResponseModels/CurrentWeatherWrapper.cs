using System.Text.Json.Serialization;

namespace PlaceService.Domain.Entities.TPAResponseModels;

public class CurrentWeatherWrapper
{
    [JsonPropertyName("current")]
    public required LocationWeatherDto LocationWeatherDto { get; set; }
}