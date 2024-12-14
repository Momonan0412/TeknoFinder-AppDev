using AppDev.API.Models.EnumValidation;
using System.ComponentModel.DataAnnotations;

namespace AppDev.API.Models.Entities
{
    public class Student
    {
        [Key]
        public Guid StudentIdentification { get; set; }
        public ICollection<Confession> Confessions { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [ValidEnum(typeof(Program))] // Automatically uses the "IsValid()" in the user-defined EnumValidation
        public StudentProgram Program { get; set; } // Use the Program enum

        [Required]
        [ValidEnum(typeof(YearLevel))] // Automatically uses the "IsValid()" in the user-defined EnumValidation
        public YearLevel YearLevel { get; set; } // Use the YearLevel enum

        [Required]
        [ValidEnum(typeof(Status))] // Automatically uses the "IsValid()" in the user-defined EnumValidation
        public Status Status { get; set; } // Use the Status enum
    }
}
