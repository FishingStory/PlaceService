using FluentValidation;
using PlaceService.Domain.Entities.Models;

namespace PlaceService.Infrastructure.Validations;

public class LocationValidator : AbstractValidator<Location>
{
    public LocationValidator()
    {
        RuleFor(l => l.Id)
            .NotNull()
            .WithMessage("Id cannot be empty");
        
        RuleFor(l => l.Name)
            .NotNull()
            .NotEmpty()
            .Length(3, 25)
            .WithMessage("Name cannot be empty");

        RuleFor(l => l.Coordinates)
            .SetValidator(new CoordinatesValidator());
        
    }
}