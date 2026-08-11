using PlaceService.Api.Extensions;
using PlaceService.Api.Filters;
using PlaceService.Api.Middleware;
using PlaceService.Api.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddOptions<PlaceServiceDbContextOptions>()
    .BindConfiguration(PlaceServiceDbContextOptions.PlaceServiceDbContextOptionsKey)
    .ValidateOnStart();

builder.Services.AddOptions<WeatherForecastOptions>()
    .BindConfiguration(WeatherForecastOptions.WeatherForecastApiOptionsKey)
    .ValidateOnStart();

builder.Services.AddOptions<WeatherForecastResilienceOptions>()
    .BindConfiguration(WeatherForecastResilienceOptions.WeatherForecastResilienceOptionsKey)
    .ValidateOnStart();


builder.Services.AddDbContextWithCustomOptions();
builder.Services.AddGeometryFactory();
builder.Services.AddWeatherHttpClient();
builder.Services.AddCoreComponents();
builder.Services.AddValidators();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<RequestValidationFilter>();
});

var app = builder.Build();


if (app.Environment.IsDevelopment()) app.MapOpenApi();

//app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
