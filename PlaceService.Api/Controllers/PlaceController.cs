using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using PlaceService.Application.DTOs;
using PlaceService.Application.IServices;

namespace PlaceService.Api.Controllers;

[Route("api/{controller}")]
[ApiController]
public class PlaceController(
    ILocationService locationService,
    GeometryFactory geometryFactory,
    [FromKeyedServices("NearbyLocationDtoValidator")]
    IValidator<RequestNearbyLocationDto> nearbyLocationDtoValidator,
    [FromKeyedServices("ResponseLocationDtoValidator")]
    IValidator<ResponseLocationDto> responseLocationDtoValidator,
    [FromKeyedServices("CreateDtoValidator")]
    IValidator<CreateLocationDto> createLocationDtoValidator,
    [FromKeyedServices("UpdateDtoValidator")]
    IValidator<UpdateLocationDto> updateLocationDtoValidator,
    [FromKeyedServices("LocationDtoValidator")]
    IValidator<LocationDto> locationDtoValidator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ResponseLocationDto>> GetLocationByCords(LocationDto locationDto)
    {
        try
        {
            await locationDtoValidator.ValidateAndThrowAsync(
                locationDto,
                HttpContext.RequestAborted);

            var coordinates = geometryFactory
                .CreatePoint(new Coordinate(locationDto.Latitude, locationDto.Longitude));

            var responseLocationDto = await locationService.GetLocation(coordinates);
            var responseValidationResult = await responseLocationDtoValidator.ValidateAsync(
                responseLocationDto,
                HttpContext.RequestAborted);

            if (!responseValidationResult.IsValid)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ValidationProblemDetails(responseValidationResult.ToDictionary())
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Response validation failed."
                    });
            }

            return Ok(responseLocationDto);
        }
        catch (ValidationException exception)
        {
            return BadRequest(new ValidationProblemDetails(
                exception.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray()))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Request validation failed."
            });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid request.",
                Detail = exception.Message
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult<HashSet<ResponseLocationDto>>> GetNearbyLocations(
        RequestNearbyLocationDto requestNearbyLocationDto)
    {
        try
        {
            await nearbyLocationDtoValidator.ValidateAndThrowAsync(
                requestNearbyLocationDto,
                HttpContext.RequestAborted);

            var responseLocationDtos = await locationService
                .GetNearbyLocations(requestNearbyLocationDto);

            foreach (var responseLocationDto in responseLocationDtos)
            {
                var responseValidationResult = await responseLocationDtoValidator.ValidateAsync(
                    responseLocationDto,
                    HttpContext.RequestAborted);

                if (!responseValidationResult.IsValid)
                {
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new ValidationProblemDetails(responseValidationResult.ToDictionary())
                        {
                            Status = StatusCodes.Status500InternalServerError,
                            Title = "Response validation failed."
                        });
                }
            }

            return Ok(responseLocationDtos.ToHashSet());
        }
        catch (ValidationException exception)
        {
            return BadRequest(new ValidationProblemDetails(
                exception.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray()))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Request validation failed."
            });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid request.",
                Detail = exception.Message
            });
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseLocationDto>> AddNewLocation(
        CreateLocationDto createLocationDto)
    {
        try
        {
            await createLocationDtoValidator.ValidateAndThrowAsync(
                createLocationDto,
                HttpContext.RequestAborted);

            var responseLocationDto = await locationService.AddLocation(createLocationDto);
            var responseValidationResult = await responseLocationDtoValidator.ValidateAsync(
                responseLocationDto,
                HttpContext.RequestAborted);

            if (!responseValidationResult.IsValid)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ValidationProblemDetails(responseValidationResult.ToDictionary())
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Response validation failed."
                    });
            }

            return Ok(responseLocationDto);
        }
        catch (ValidationException exception)
        {
            return BadRequest(new ValidationProblemDetails(
                exception.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray()))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Request validation failed."
            });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid request.",
                Detail = exception.Message
            });
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateLocation(UpdateLocationDto updateLocationDto)
    {
        try
        {
            await updateLocationDtoValidator.ValidateAndThrowAsync(
                updateLocationDto,
                HttpContext.RequestAborted);

            var responseLocationDto = await locationService.UpdateLocation(updateLocationDto);
            var responseValidationResult = await responseLocationDtoValidator.ValidateAsync(
                responseLocationDto,
                HttpContext.RequestAborted);

            if (!responseValidationResult.IsValid)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ValidationProblemDetails(responseValidationResult.ToDictionary())
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Response validation failed."
                    });
            }

            return Ok(responseLocationDto);
        }
        catch (ValidationException exception)
        {
            return BadRequest(new ValidationProblemDetails(
                exception.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray()))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Request validation failed."
            });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid request.",
                Detail = exception.Message
            });
        }
    }
}
