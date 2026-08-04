namespace PlaceService.Api.Options;

public class PlaceServiceDbContextOptions
{
    public const string PlaceServiceDbContextOptionsKey = "Location";
    public string? DefaultConnection { get; set; }
}