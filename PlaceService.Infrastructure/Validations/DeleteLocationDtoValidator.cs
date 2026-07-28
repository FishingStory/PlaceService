using FluentValidation;
using PlaceService.Application.DTOs;

namespace PlaceService.Infrastructure.Validations;

public class DeleteLocationDtoValidator<T> : AbstractValidator<T> where T : DeleteLocationDto
{
    public DeleteLocationDtoValidator()
    {
        RuleFor(l => l.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id cannot be empty");
        
    }
}