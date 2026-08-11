using FluentValidation;
using PlaceService.Application.DTOs;

namespace PlaceService.Infrastructure.Validations;

public class ResponseLocationDtoValidator : AbstractValidator<ResponseLocationDto>
{
    public ResponseLocationDtoValidator()
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

        RuleFor(l => l.AvgTemperature)
            .Must(double.IsFinite)
            .WithMessage("Average temperature must be a valid number.");

        RuleFor(l => l.AvgPressure)
            .Must(double.IsFinite)
            .WithMessage("Average pressure must be a valid number.")
            .GreaterThan(0)
            .WithMessage("Average pressure must be greater than 0.");

        RuleFor(l => l.AvgWindSpeed)
            .Must(double.IsFinite)
            .WithMessage("Average wind speed must be a valid number.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Average wind speed cannot be negative.");
    }
}