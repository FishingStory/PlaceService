using FluentValidation;
using PlaceService.Application.DTOs;

namespace PlaceService.Infrastructure.Validations;

public class CreateLocationDtoValidator : BaseDtoValidator<CreateLocationDto>
{
    public CreateLocationDtoValidator()
    {
        RuleFor(l => l.Name)
            .NotNull()
            .NotEmpty()
            .Length(3, 25)
            .WithMessage("Name cannot be empty");
    }
}