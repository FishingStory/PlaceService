using FluentValidation;
using PlaceService.Application.DTOs;

namespace PlaceService.Infrastructure.Validations;

public class BaseDtoValidator<T> : AbstractValidator<T> 
    where T : LocationDto
{
    public BaseDtoValidator()
    {
        RuleFor(point => point.Latitude)
            .NotNull()
            .Must(double.IsFinite)
            .WithMessage("Latitude must be a valid number.")
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90.");
        
        RuleFor(point => point.Longitude)
            .NotNull()
            .Must(double.IsFinite)
            .WithMessage("Longitude must be a valid number.")
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180.");
    }
}