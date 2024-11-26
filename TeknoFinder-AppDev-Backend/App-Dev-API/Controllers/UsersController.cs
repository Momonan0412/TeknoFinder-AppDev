using AppDev.API.Data;
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
        private readonly ApplicationDbContext applicationDbContext;
        public UsersController(ApplicationDbContext applicationDbContext) =>
            (this.applicationDbContext) = (applicationDbContext);


        [HttpGet]
        public IActionResult GetAllUsers() {
            return Ok(applicationDbContext.Users.ToList()); // 200
        }

        //[HttpPost]
        //public IActionResult AddUser(AddUserDTO addUserDTO)
        //{
        //    var newUser = UserMapper.ConvertFromDTO(addUserDTO);
        //    applicationDbContext.Users.Add(newUser);
        //    applicationDbContext.SaveChanges();
        //    return CreatedAtAction( // 201
        //        nameof(GetUserById),
        //        new { id = newUser.UserIdentification },
        //        newUser
        //        );
        //}

        [HttpPut("{id:guid}/update")]
        public IActionResult UpdateUserById(Guid id, UpdateUserDTO updateUserDTO) {
            var user = applicationDbContext.Users.Find(id);
            if(user is null) return NotFound();
            if (!string.IsNullOrEmpty(updateUserDTO.Email))
            {
                user.Email = updateUserDTO.Email;
            }
            if (!string.IsNullOrEmpty(updateUserDTO.Password))
            {
                user.Password = updateUserDTO.Password;
            }
            if (user.IsActive != updateUserDTO.IsActive)
            {
                user.IsActive = updateUserDTO.IsActive;
            }
            applicationDbContext.SaveChanges();
            return Ok(user);
        }
        [HttpPut("{id:guid}/toggle-alive")]
        public IActionResult UpdateUserIsAlive(Guid id) {
            var user = applicationDbContext.Users.Find(id);
            if (user is null) return NotFound();
            user.IsActive = !user.IsActive;
            applicationDbContext.SaveChanges();
            return Ok(user);
        }
    }
}
