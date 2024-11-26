using AppDev.API.Data;
using AppDev.API.Models.DataTransferObject.User;
using AppDev.API.Models.DataTransferObject.UserAndStudent;
using AppDev.API.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService jwtService;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly UserService userService;
        public AccountController(JwtService jwtService, ApplicationDbContext applicationDbContext) => (this.jwtService, this.applicationDbContext, userService) = (jwtService, applicationDbContext, new UserService(applicationDbContext));


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginUserDTO loginUserDTO)
        {
            var result = await jwtService.Authenticate(loginUserDTO);
            if (result == null)
            {
                return Unauthorized();
            }
            return result;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> AddUserAndStudent(UserAndStudentDTO userAndStudentDTO)
        {
            try
            {
                var result = await userService.AddUserAndStudentAsync(userAndStudentDTO);
                if (!result.Success) return BadRequest(result.ErrorMessage);

                return CreatedAtAction(
                    nameof(GetUserById),
                    new { id = result.user.StudentIdentification },
                    result.user
                    );
            }
            catch (Exception ex)
            {
                // Log the exception details for more information
                // This will help to see the inner exception message
                return BadRequest("An error occurred: " + ex.Message + "\nInner Exception: " + ex.InnerException?.Message);
            }
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = applicationDbContext.Users.Find(id);
            return (user is null) ? Ok() : NotFound();
        }
    }
}
