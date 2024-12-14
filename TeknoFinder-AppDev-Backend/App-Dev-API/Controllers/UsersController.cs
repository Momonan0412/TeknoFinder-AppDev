using AppDev.API.Data;
using AppDev.API.Interface;
using AppDev.API.Models.DataTransferObject.User;
using AppDev.API.Models.DataTransferObject.UserAndStudent;
using AppDev.API.Models.Mapper;
using AppDev.API.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) =>
            (this._userService) = (userService);


        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync() {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("{id:guid}/update")]
        public IActionResult UpdateUserByIdAsync(Guid id, UpdateUserDTO updateUserDTO) {
            var updatedUser = _userService.UpdateUserAsync(id, updateUserDTO);
            return updatedUser is null ? NotFound() : Ok(updatedUser);
        }
        [HttpPut("{id:guid}/toggle-alive")]
        public async Task<IActionResult> UpdateUserIsAliveAsync(Guid id)
        {
            var toggledUser = await _userService.ToggleUserIsActiveAsync(id);
            return toggledUser is null ? NotFound() : Ok(toggledUser);
        }
    }
}
