using AppDev.API.Models.DataTransferObject.Waypoint;
using AppDev.API.Models.Entities;

namespace AppDev.API.Interface
{
    public interface IWaypointService
    {
        Task<IEnumerable<Waypoint>> GetAllWaypointsAsync();
        Task<Waypoint?> GetWaypointByIdAsync(Guid id);
        Task<Waypoint> AddWaypointAsync(WaypointDTO waypointDTO);
        Task<Waypoint?> UpdateWaypointAsync(Guid id, WaypointDTO waypointDTO);
        Task<bool> DeleteWaypointAsync(Guid id);
    }
}
