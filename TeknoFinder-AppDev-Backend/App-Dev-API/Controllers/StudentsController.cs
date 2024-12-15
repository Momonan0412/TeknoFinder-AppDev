using AppDev.API.Data;
using Microsoft.AspNetCore.Mvc;
using AppDev.API.Models.Entities;
using AppDev.API.Models.Mapper;
using Microsoft.AspNetCore.Authorization;
using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Interface;
using AppDev.API.Models.Service;
namespace AppDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IUserService _IUserService;
        private readonly ICurrentUserService _currentUserService;
        public StudentsController(IStudentService studentService, 
            ICurrentUserService currentUserService,
            IUserService userService
            ) => 
            (_studentService, _currentUserService, _IUserService) = (studentService, currentUserService, userService);
        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students); // 200 OK
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudentByIdAsync(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            var userId = _currentUserService.UserId;
            var user = await _IUserService.GetUserByIdAsync(userId);
            return student is null ? NotFound() : Ok(new StudentUsernameDTO { 
                Username = user.Username,
                FirstName = student.FirstName,
                LastName = student.LastName,
                StudentNumber = student.StudentNumber,
                Program = student.Program,
                YearLevel = student.YearLevel,
            });
        }

        [HttpPut("{id:guid}/update")]
        public async Task<IActionResult> UpdateStudentAsync(Guid id, UpdateStudentDTO updateStudentDTO)
        {
            var success = await _studentService.UpdateStudentAsync(id, updateStudentDTO);
            return success ? Ok() : NotFound();
        }
    }
}
