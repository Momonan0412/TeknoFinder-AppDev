using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppDev.API.Models.DataTransferObject.User
{
    public class LoginUserDTO
    {

        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Not a valid email address.")]
        [MaxLength(256)] // Length constraint for performance and consistency
        public new string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public new string Password { get; set; }
    }
}
