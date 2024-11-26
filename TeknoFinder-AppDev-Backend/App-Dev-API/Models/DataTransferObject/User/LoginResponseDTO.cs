using System.ComponentModel.DataAnnotations;

namespace AppDev.API.Models.DataTransferObject.User
{
    public class LoginResponseDTO
    {
        public new string Email { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
