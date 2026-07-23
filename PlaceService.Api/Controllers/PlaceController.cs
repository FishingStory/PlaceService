using Microsoft.AspNetCore.Mvc;
using PlaceService.Application.DTOs;

namespace PlaceService.Api.Controllers;

[Route("api/place")]
[ApiController]
public class PlaceController : ControllerBase
{
    
    [HttpGet]
    public ActionResult<ResponseLocationDto> GetLocationByCords(LocationDto locationDto)
    {
        return Ok();
    }
    
    
    [HttpGet]
    public ActionResult<HashSet<ResponseLocationDto>> GetNearbyLocations(RequestNearbyLocationDto requestNearbyLocationDto)
    {
        return Ok();
    }
    
    [HttpPost]
    public ActionResult<ResponseLocationDto> AddNewLocation(CreateLocationDto createLocationDto)
    {
        return Ok();
    }
    
    [HttpPut]
    public ActionResult UpdateLocation(UpdateLocationDto updateLocationDto)
    {
        return Ok();
    }
}