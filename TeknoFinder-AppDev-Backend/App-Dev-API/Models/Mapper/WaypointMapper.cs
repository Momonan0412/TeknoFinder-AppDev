using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.DataTransferObject.Waypoint;
using AppDev.API.Models.Entities;
using AppDev.API.Models;
using System.Drawing;
using AppDev.API.Models.EnumValidation;

namespace AppDev.API.Models.Mapper
{
    public class WaypointMapper
    {
        public static WaypointDTO ConvertToDTO(Waypoint waypoint)
        {
            return new WaypointDTO
            {
                WaypointName = waypoint.WaypointName,
                WaypointType = waypoint.WaypointType,
                PointX = waypoint.PointX,
                PointY = waypoint.PointY,
                Floor = waypoint.Floor,
                Building = waypoint.Building.ToString(),
            };
        }

        public static Waypoint ConvertFromDTO(WaypointDTO dto)
        {
            return new Waypoint
            {
                WaypointName = dto.WaypointName,
                WaypointType = dto.WaypointType,
                PointX = dto.PointX,
                PointY = dto.PointY,
                Floor = dto.Floor,
                Building = Enum.Parse<Building>(dto.Building)
            };
        }
    }
}
