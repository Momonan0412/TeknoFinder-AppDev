namespace AppDev.API.Models.DataTransferObject.User
{
    public class LoginNumberResponseDTO
    {
        public new string StudentNumber { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public Guid UserId { get; set; } // Add UserId to DTO
        public Guid? StudentId { get; set; } // Add StudentId to DTO
    }
}
