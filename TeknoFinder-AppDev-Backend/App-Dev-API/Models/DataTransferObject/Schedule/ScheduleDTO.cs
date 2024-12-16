using AppDev.API.Models.EnumValidation;
using System.ComponentModel.DataAnnotations;

namespace AppDev.API.Models.DataTransferObject.Schedule
{
    public class ScheduleDTO
    {
        public string SubjectTitle { get; set; }
        public string Section { get; set; }
        public string Classroom { get; set; }
        public Day Day { get; set; }
        public TimeOnly StartsAt { get; set; }
        public TimeOnly EndsAt { get; set; }
    }
}
