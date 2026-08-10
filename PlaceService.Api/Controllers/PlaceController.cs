using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using PlaceService.Application.DTOs;
using PlaceService.Application.IServices;

namespace PlaceService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaceController(
    ILocationService locationService,
    GeometryFactory geometryFactory) : ControllerBase
{
    [HttpGet("by-coordinates")]
    public async Task<ActionResult<ResponseLocationDto>> GetLocationByCords(
        [FromQuery] LocationDto locationDto)
    {
        var coordinates = geometryFactory
            .CreatePoint(new Coordinate(locationDto.Longitude, locationDto.Latitude));

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
    public async Task<ActionResult<HashSet<ResponseLocationDto>>> GetNearbyLocations(
        [FromQuery] RequestNearbyLocationDto requestNearbyLocationDto)
    {
        var responseLocationDtos = await locationService
            .GetNearbyLocations(requestNearbyLocationDto);

        return Ok(responseLocationDtos.ToHashSet());
    }

    [HttpPost]
    public async Task<ActionResult<ResponseLocationDto>> AddNewLocation(
        [FromBody] CreateLocationDto createLocationDto)
    {
        var responseLocationDto = await locationService.AddLocation(createLocationDto);

        return Ok(responseLocationDto);
    }

    [HttpPut]
    public async Task<ActionResult<ResponseLocationDto>> UpdateLocation(
        [FromBody] UpdateLocationDto updateLocationDto)
    {
        var responseLocationDto = await locationService.UpdateLocation(updateLocationDto);

        return Ok(responseLocationDto);
    }
}
