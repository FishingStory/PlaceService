using FluentValidation;
using PlaceService.Application.DTOs;

namespace PlaceService.Infrastructure.Validations;

public class NearbyLocationDtoValidator : BaseDtoValidator<RequestNearbyLocationDto>
{
    public NearbyLocationDtoValidator()
    {
        RuleFor(x => x.Distance)
            .ExclusiveBetween(0, 50000)
            .WithMessage("Distance must be between 0 and 50000 meters");
    }
}