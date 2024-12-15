using AppDev.API.Data;
using AppDev.API.Interface;
using AppDev.API.Models.DataTransferObject.Waypoint;
using AppDev.API.Models.Entities;
using AppDev.API.Models.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        public async Task<Waypoint> AddWaypointAsync(WaypointDTO waypointDTO)
        {
            try
            {
                // Convert DTO to entity
                var newWaypoint = WaypointMapper.ConvertFromDTO(waypointDTO);

                // Check for duplicate WaypointName
                var existingWaypoint = await _applicationDbContext.Waypoints
                    .FirstOrDefaultAsync(w => w.WaypointName == newWaypoint.WaypointName);

                if (existingWaypoint != null)
                {
                    // Throw a custom exception or return a specific error
                    throw new InvalidOperationException($"A waypoint with the name '{newWaypoint.WaypointName}' already exists.");
                }

                // Add new waypoint to the context
                _applicationDbContext.Waypoints.Add(newWaypoint);

                // Save changes to the database
                await _applicationDbContext.SaveChangesAsync();

                // Return the newly added waypoint
                return newWaypoint;
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database-related exceptions (e.g., constraint violations)
                // Log the error or rethrow a custom exception with more details
                Debug.WriteLine($"Database error: {dbEx.Message}");
                throw new ApplicationException("An error occurred while saving the waypoint.", dbEx);
            }
            catch (InvalidOperationException invalidEx)
            {
                // Handle known validation errors (e.g., duplicate waypoint)
                Debug.WriteLine($"Validation error: {invalidEx.Message}");
                throw new ApplicationException(invalidEx.Message);
            }
            catch (Exception ex)
            {
                // Handle any other general exceptions
                Debug.WriteLine($"An error occurred: {ex.Message}");
                throw new ApplicationException("An unexpected error occurred.", ex);
            }
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
