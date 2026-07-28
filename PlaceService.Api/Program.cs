using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlaceService.Api.Extensions;
using PlaceService.Api.Options;
using PlaceService.Application.IServices;
using PlaceService.Domain.IRepositories;
using PlaceService.Application.IMappers;
using PlaceService.Application.Mappers;
using PlaceService.Infrastructure.DbContexts;
using PlaceService.Infrastructure.Repositories;
using PlaceService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

PlaceServiceDbContextOptions options = new();
builder.Configuration.GetSection("Location").Bind(options);

builder.Services.AddDbContextWithCustomOptions(options);
builder.Services.AddGeometryFactory();


// move into the separate Extension
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddTransient<ILocationMapper, LocationMapper>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.Run();

