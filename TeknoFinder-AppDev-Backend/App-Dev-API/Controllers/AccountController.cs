using AppDev.API.Data;
using AppDev.API.Interface;
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
        private readonly IUserService _userService;
        public AccountController(JwtService jwtService, ApplicationDbContext applicationDbContext, IUserService userService) 
            => 
            (this.jwtService, this.applicationDbContext, _userService) = 
            (jwtService, applicationDbContext, userService);


        [AllowAnonymous]
        [HttpPost("LoginEmail")]
        public async Task<ActionResult<LoginEmailResponseDTO>> Login(LoginEmailDTO loginUserDTO)
        {
            var result = await jwtService.Authenticate(loginUserDTO);
            if (result == null)
            {
                return Unauthorized();
            }
            return result;
        }
        [AllowAnonymous]
        [HttpPost("LoginStudentNumber")]
        public async Task<ActionResult<LoginNumberResponseDTO>> Login(LoginNumberDTO loginUserDTO)
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
                var result = await _userService.AddUserAndStudentAsync(userAndStudentDTO);
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
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // Return 404 if user is not found
            }
            return Ok(user); // Return 200 OK with the user data
        }
    }
}
