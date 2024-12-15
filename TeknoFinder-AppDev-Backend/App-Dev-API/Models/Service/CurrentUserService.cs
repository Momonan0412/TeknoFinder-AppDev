using AppDev.API.Interface;

namespace AppDev.API.Models.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid UserId { get; }
        public Guid StudentId { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            // Safely parse UserId and StudentId from string to Guid
            if (user != null)
            {
                Guid.TryParse(user.FindFirst("UserId")?.Value, out var userId);
                Guid.TryParse(user.FindFirst("StudentId")?.Value, out var studentId);

                UserId = userId;
                StudentId = studentId;
            }
        }
    }
}
