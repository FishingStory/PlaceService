using Microsoft.AspNetCore.Mvc;

namespace PlaceService.Api.Middleware;

public sealed class GlobalExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ArgumentException exception)
        {
            if (context.Response.HasStarted)
                throw;

            await WriteProblemDetailsAsync(context, exception.Message);
        }
    }

    private static async Task WriteProblemDetailsAsync(
        HttpContext context,
        string detail)
    {
        context.Response.Clear();
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Invalid request.",
            Detail = detail,
            Instance = context.Request.Path
        });
    }
}
