using FluentValidation;
using NetTopologySuite.Geometries;

namespace PlaceService.Infrastructure.Validations;

public class CoordinatesValidator : AbstractValidator<Point>
{
    public CoordinatesValidator()
    {
        RuleFor(point => point)
            .NotNull()
            .WithMessage("Coordinates cannot be empty.");

        RuleFor(point => point.IsEmpty)
            .Equal(false)
            .WithMessage("Coordinates cannot be empty.");

        RuleFor(point => point.X)
            .Must(double.IsFinite)
            .WithMessage("Longitude must be a valid number.")
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180.");

        RuleFor(point => point.Y)
            .Must(double.IsFinite)
            .WithMessage("Latitude must be a valid number.")
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90.");

        RuleFor(point => point.SRID)
            .NotEmpty()
            .Equal(4326)
            .WithMessage("SRID is empty or inappropriate.");
    }
}