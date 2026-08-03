using FluentValidation;
using PlaceService.Application.DTOs;

namespace PlaceService.Infrastructure.Validations;

public class UpdateLocationDtoValidator : AbstractValidator<UpdateLocationDto>
{
    public UpdateLocationDtoValidator()
    {
        
        RuleFor(l => l.Id)
            .NotNull()
            .WithMessage("Id cannot be empty");
        
        RuleFor(l => l.Name)
            .NotNull()
            .NotEmpty()
            .Length(3, 25)
            .WithMessage("Name cannot be empty");
    }
}