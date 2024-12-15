using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccessClaimUtil : ControllerBase
    {
        [HttpGet]
        [Route("GetUserInfo")]
        public IActionResult GetUserInfo()
        {
            var userId = User.FindFirst("UserId")?.Value; // Extract UserId from claims
            var studentId = User.FindFirst("StudentId")?.Value; // Extract StudentId from claims

            if (userId == null || studentId == null)
            {
                return Unauthorized("Missing claims in token.");
            }

            return Ok(new
            {
                UserId = userId,
                StudentId = studentId
            });
        }
    }
}
