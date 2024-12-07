using System.ComponentModel.DataAnnotations;
using System.Drawing;
using AppDev.API.Models;
using AppDev.API.Models.EnumValidation;

namespace AppDev.API.Models.DataTransferObject.Waypoint
{
    public class WaypointDTO
    {
        [Required]
        [StringLength(200)]
        public string WaypointName { get; set; }
        [Required]
        [StringLength(200)]
        public string WaypointType { get; set; }
        [Required]
        public float PointX { get; set; }
        [Required]
        public float PointY { get; set; }
        public int Floor { get; set; }
        [Required]
        public String Building { get; set; }
        public override string ToString()
        {
            return $"User: WaypointName = {WaypointName}, WaypointType = {WaypointType}, Waypoint Coordinates = {PointX}, {PointY}";
        }
    }
}
