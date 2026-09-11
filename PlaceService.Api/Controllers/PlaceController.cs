using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using PlaceService.Application.DTOs;
using PlaceService.Application.IMappers;
using PlaceService.Application.IServices;

namespace PlaceService.Api.Controllers;

[Route("api/place-service")]
[ApiController]
public class PlaceController(
    ILocationService locationService,
    ILocationMapper locationMapper,
    GeometryFactory geometryFactory) : ControllerBase
{
    [HttpGet("by-coordinates")]
    public async Task<ActionResult<ResponseLocationDto>> GetLocationByCords(
        [FromQuery] LocationDto locationDto)
    {
        var coordinates = locationMapper.MapCoordinatesToPoint(locationDto.Longitude, locationDto.Latitude);

        var responseLocationDto = await locationService.GetLocation(coordinates);

        return Ok(responseLocationDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResponseLocationDto>> GetLocationById(
        [FromRoute] Guid id)
    {
        var responseLocationDto = await locationService.GetLocationById(id);

        return Ok(responseLocationDto);
    }

    [HttpGet("nearby")]
    public async Task<ActionResult<IEnumerable<ResponseLocationDto>>> GetNearbyLocations(
        [FromQuery] RequestNearbyLocationDto requestNearbyLocationDto)
    {
        var responseLocationDtos = await locationService
            .GetNearbyLocations(requestNearbyLocationDto);

        return Ok(responseLocationDtos.ToHashSet());
    }

    [HttpPost("create")]
    public async Task<ActionResult<ResponseLocationDto>> AddNewLocation(
        [FromBody] CreateLocationDto createLocationDto)
    {
        var responseLocationDto = await locationService.AddLocation(createLocationDto);

        return Ok(responseLocationDto);
    }

    [HttpPut("update")]
    public async Task<ActionResult<ResponseLocationDto>> UpdateLocation(
        [FromBody] UpdateLocationDto updateLocationDto)
    {
        var responseLocationDto = await locationService.UpdateLocation(updateLocationDto);

        return Ok(responseLocationDto);
    }
}
