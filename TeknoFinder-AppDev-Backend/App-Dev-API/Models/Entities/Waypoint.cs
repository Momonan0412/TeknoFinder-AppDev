using System.ComponentModel.DataAnnotations;
using System.Drawing;
using AppDev.API.Models;

namespace AppDev.API.Models.Entities
{
    public class Waypoint
    {
        [Key]
        public Guid WaypointId { get; set; }
        [Required]
        [StringLength(200)]
        public string WaypointName { get; set; }
        [Required]
        [StringLength(200)]
        public string WaypointType { get; set; }
        [Required]
        public float PointX { get; set; } = 0.0f;
        public float PointY { get; set; } = 0.0f;


        // int Floor 
        // Enum List_Building Enum 
    }
}
