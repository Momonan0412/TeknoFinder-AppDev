using AppDev.API.Data;
using AppDev.API.Interface;
using AppDev.API.Models.DataTransferObject.Waypoint;
using AppDev.API.Models.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AppDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaypointsController : ControllerBase
    {
        private readonly IWaypointService _waypointService;

        public WaypointsController(IWaypointService waypointService)
        {
            _waypointService = waypointService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWaypointsAsync()
        {
            var waypoints = await _waypointService.GetAllWaypointsAsync();
            return Ok(waypoints);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetWaypointByIdAsync(Guid id)
        {
            var waypoint = await _waypointService.GetWaypointByIdAsync(id);
            return waypoint is null ? NotFound() : Ok(waypoint);
        }

        [HttpPost]
        public async Task <IActionResult> AddWaypointAsync(WaypointDTO waypointDTO)
        {
            var newWaypoint = await _waypointService.AddWaypointAsync(waypointDTO);
            return CreatedAtAction(
                nameof(GetWaypointByIdAsync),
                new { id = newWaypoint.WaypointId },
                newWaypoint
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateWaypointAsync(Guid id, WaypointDTO waypointDTO)
        {
            var updatedWaypoint = await _waypointService.UpdateWaypointAsync(id, waypointDTO);
            return updatedWaypoint is null ? NotFound() : Ok(updatedWaypoint);
        }

        [HttpDelete("{id:guid}")]
        public async Task <IActionResult> DeleteWaypointAsync(Guid id)
        {
            var deleted = await _waypointService.DeleteWaypointAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}

