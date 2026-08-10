using System.Collections;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PlaceService.Api.Filters;

public sealed class ResponseValidationFilter() : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(
        ResultExecutingContext context,
        ResultExecutionDelegate next)
    {
        if (context.Result is not ObjectResult { Value: not null } objectResult)
        {
            await next();
            return;
        }

        var statusCode =
            objectResult.StatusCode ?? StatusCodes.Status200OK;

        if (statusCode is < 200 or >= 300)
        {
            await next();
            return;
        }

        var response = objectResult.Value;
        var failures = new List<ValidationFailure>();

        var directResult = await TryValidateAsync(response, context);

        if (directResult is not null)
        {
            failures.AddRange(directResult.Errors);
        }
        else if (response is IEnumerable collection &&
                 response is not string)
        {
            foreach (var element in collection)
            {
                if (element is null)
                    continue;

                var elementResult =
                    await TryValidateAsync(element, context);

                if (elementResult is not null)
                    failures.AddRange(elementResult.Errors);
            }
        }

        if (failures.Count > 0)
        {
            context.Result = new ObjectResult(
                new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An internal server error occurred."
                })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        await next();
    }

    private static async Task<ValidationResult?> TryValidateAsync(
        object value,
        ResultExecutingContext context)
    {
        var validatorType = typeof(IValidator<>)
            .MakeGenericType(value.GetType());

        if (context.HttpContext.RequestServices
                .GetService(validatorType) is not IValidator validator)
        {
            return null;
        }

        return await validator.ValidateAsync(
            new ValidationContext<object>(value),
            context.HttpContext.RequestAborted);
    }
}