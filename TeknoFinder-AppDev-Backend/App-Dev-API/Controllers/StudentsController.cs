using AppDev.API.Data;
using Microsoft.AspNetCore.Mvc;
using AppDev.API.Models.Entities;
using AppDev.API.Models.Mapper;
using Microsoft.AspNetCore.Authorization;
using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Interface;
namespace AppDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService) => _studentService = studentService;
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
            return student is null ? NotFound() : Ok(student);
        }

        [HttpPut("{id:guid}/update")]
        public async Task<IActionResult> UpdateStudentAsync(Guid id, UpdateStudentDTO updateStudentDTO)
        {
            var success = await _studentService.UpdateStudentAsync(id, updateStudentDTO);
            return success ? Ok() : NotFound();
        }
    }
}
