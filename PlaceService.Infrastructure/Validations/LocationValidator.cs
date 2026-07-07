using FluentValidation;
using PlaceService.Domain.Entities.Models;

namespace PlaceService.Infrastructure.Validations;

public class LocationValidator : AbstractValidator<Location>
{
    public LocationValidator()
    {
        RuleFor(l => l.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id cannot be empty");
        
        RuleFor(l => l.Name)
            .NotNull()
            .NotEmpty()
            .Length(3, 25)
            .WithMessage("Name cannot be empty");

        RuleFor(l => l.Coordinates)
            .SetValidator(new CoordinatesValidator());
        
        RuleFor(l => l.AvgTemperature)
            .NotNull()
            .NotEmpty()
            .InclusiveBetween(-1, 32)
            .WithMessage("Avg temperature cannot be empty");
        
        RuleFor(l => l.AvgPressure)
            .NotNull()
            .NotEmpty()
            .InclusiveBetween(950, 1050)
            .WithMessage("Avg pressure cannot be empty");
        
    }
}