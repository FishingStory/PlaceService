using PlaceService.Api.Extensions;
using PlaceService.Api.Options;
using PlaceService.Application.IServices;
using PlaceService.Domain.IRepositories;
using PlaceService.Application.IMappers;
using PlaceService.Application.Mappers;
using PlaceService.Infrastructure.Repositories;
using PlaceService.Infrastructure.Services;

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

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.Run();

