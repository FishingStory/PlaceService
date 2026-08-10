using FluentValidation;
using PlaceService.Application.DTOs;
using PlaceService.Infrastructure.Validations;
using Location = PlaceService.Domain.Entities.Models.Location;

namespace PlaceService.Api.Extensions;

public static class AddValidatorsExtension
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<RequestNearbyLocationDto>, NearbyLocationDtoValidator>();
        services.AddTransient<IValidator<ResponseLocationDto>, ResponseLocationDtoValidator>();
         
        services.AddTransient<IValidator<LocationDto>, BaseDtoValidator<LocationDto>>();

        services.AddTransient<IValidator<CreateLocationDto>, CreateLocationDtoValidator>();
        services.AddTransient<IValidator<UpdateLocationDto>, UpdateLocationDtoValidator>();
        services.AddTransient<IValidator<DeleteLocationDto>, DeleteLocationDtoValidator<DeleteLocationDto>>();

        services.AddTransient<IValidator<Location>, LocationValidator>();

        return services;
    }
}