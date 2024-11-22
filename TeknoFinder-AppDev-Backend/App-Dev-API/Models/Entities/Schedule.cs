using AppDev.API.Models.EnumValidation;
using System.ComponentModel.DataAnnotations;

namespace AppDev.API.Models.Entities
{
    public class Schedule
    {
        public Guid ScheduleId { get; set; }
        [Required]
        [MaxLength(100)]
        public string SubjectTitle { get; set; }
        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        public string Section {  get; set; }
        [Required]
        public Classroom Classroom { get; set; }
        [Required]
        public Day Day { get; set; }
        [Required]
        public TimeOnly StartsAt { get; set; }
        [Required]
        public TimeOnly EndsAt { get; set; }


    }
}
