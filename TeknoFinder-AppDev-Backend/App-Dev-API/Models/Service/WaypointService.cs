using AppDev.API.Data;
using AppDev.API.Interface;
using AppDev.API.Models.DataTransferObject.Waypoint;
using AppDev.API.Models.Entities;
using AppDev.API.Models.Mapper;
using Microsoft.EntityFrameworkCore;

namespace AppDev.API.Models.Service
{
    public class WaypointService : IWaypointService
    {
        private ApplicationDbContext _applicationDbContext;

        public WaypointService(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;


        public async Task<IEnumerable<Waypoint>> GetAllWaypointsAsync()
        {
            return await _applicationDbContext.Waypoints.ToListAsync();
        }

        public async Task <Waypoint?> GetWaypointByIdAsync(Guid id)
        {
            return await _applicationDbContext.Waypoints.FindAsync(id);
        }

        public async Task <Waypoint> AddWaypointAsync(WaypointDTO waypointDTO)
        {
            var newWaypoint = WaypointMapper.ConvertFromDTO(waypointDTO);
            _applicationDbContext.Waypoints.Add(newWaypoint);
            await _applicationDbContext.SaveChangesAsync();
            return newWaypoint;
        }

        public async Task<Waypoint?> UpdateWaypointAsync(Guid id, WaypointDTO waypointDTO)
        {
            var waypoint = _applicationDbContext.Waypoints.Find(id);
            if (waypoint == null) return null;

            var updatedWaypoint = WaypointMapper.ConvertFromDTO(waypointDTO);

            waypoint.WaypointName = updatedWaypoint.WaypointName ?? waypoint.WaypointName;
            waypoint.WaypointType = updatedWaypoint.WaypointType ?? waypoint.WaypointType;
            waypoint.PointX = updatedWaypoint.PointX;
            waypoint.PointY = updatedWaypoint.PointY;

            await _applicationDbContext.SaveChangesAsync();
            return waypoint;
        }

        public async Task<bool> DeleteWaypointAsync(Guid id)
        {
            var waypoint = _applicationDbContext.Waypoints.Find(id);
            if (waypoint == null) return false;

            _applicationDbContext.Waypoints.Remove(waypoint);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
