using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace AppDev.API.Models.DataTransferObject.User
{
    public class LoginNumberDTO
    {
        [Required(ErrorMessage = "Student number address is required.")]
        //[SwaggerSchema("The student's unique identifier", Example = "12345678")]
        [MaxLength(256)] // Length constraint for performance and consistency
        public new string StudentNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public new string Password { get; set; }
    }
}
