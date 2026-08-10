using PlaceService.Api.Extensions;
using PlaceService.Api.Filters;
using PlaceService.Api.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddOptions<PlaceServiceDbContextOptions>()
    .BindConfiguration(PlaceServiceDbContextOptions.PlaceServiceDbContextOptionsKey)
    .ValidateOnStart();

builder.Services.AddOptions<WeatherForecastOptions>()
    .BindConfiguration(WeatherForecastOptions.WeatherForecastApiOptionsKey)
    .ValidateOnStart();


builder.Services.AddDbContextWithCustomOptions();
builder.Services.AddGeometryFactory();
builder.Services.AddWeatherHttpClient();
builder.Services.AddCoreComponents();
builder.Services.AddValidators();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<RequestValidationFilter>();
    options.Filters.Add<ResponseValidationFilter>();
});

var app = builder.Build();


if (app.Environment.IsDevelopment()) app.MapOpenApi();

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
