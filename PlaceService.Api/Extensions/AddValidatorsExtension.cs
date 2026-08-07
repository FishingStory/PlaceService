using FluentValidation;
using PlaceService.Application.DTOs;
using PlaceService.Infrastructure.Validations;
using Location = PlaceService.Domain.Entities.Models.Location;

namespace PlaceService.Api.Extensions;

public static class AddValidatorsExtension
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddKeyedTransient<IValidator<RequestNearbyLocationDto>, NearbyLocationDtoValidator>(
            "NearbyLocationDtoValidator");
        services.AddKeyedTransient<IValidator<ResponseLocationDto>, ResponseLocationDtoValidator>(
            "ResponseLocationDtoValidator");
        services.AddKeyedTransient<IValidator<LocationDto>, BaseDtoValidator<LocationDto>>(
            "LocationDtoValidator");
        
        services.AddKeyedTransient<IValidator<CreateLocationDto>, CreateLocationDtoValidator>(
            "CreateDtoValidator");
        services.AddKeyedTransient<IValidator<UpdateLocationDto>, UpdateLocationDtoValidator>(
            "UpdateDtoValidator");
        services.AddKeyedTransient<IValidator<DeleteLocationDto>, DeleteLocationDtoValidator<DeleteLocationDto>>(
            "DeleteDtoValidator");
        
        services.AddKeyedTransient<IValidator<Location>, LocationValidator>("LocationValidator");
        
        return services;
    }
}
