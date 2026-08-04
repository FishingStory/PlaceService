using PlaceService.Application.IMappers;
using PlaceService.Application.IServices;
using PlaceService.Application.Mappers;
using PlaceService.Domain.IRepositories;
using PlaceService.Infrastructure.Repositories;
using PlaceService.Infrastructure.Services;

namespace PlaceService.Api.Extensions;

public static class AddCoreComponentsExtension
{
    public static IServiceCollection AddCoreComponents(this IServiceCollection services)
    {
        
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddTransient<ILocationMapper, LocationMapper>();
        
        return services;
    }
}