using AppDev.API.Data;
using AppDev.API.Models.DataTransferObject.Confession;
using AppDev.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.DataTransferObject.Schedule;
using AppDev.API.Models.EnumValidation;

// pagination : https:\//dev.to/bytehide/pagination-in-c-complete-guide-with-easy-code-examples-3ma2
namespace AppDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfessionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ConfessionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // debug purposes cuz idk how search without id
        [HttpGet("DebugPurposes")]
        public async Task<IActionResult> GetAllConfessionsWithId()
        {
            var confessions = await _context.Confessions
                .Include(c => c.Student)
                .ToListAsync();
            return Ok(confessions);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConfessions()
        {
            var confessions = await _context.Confessions.Include(c => c.Student).Select(c =>
             new GetAllConfessionDTO()
             {
                 StudentId = c.StudentId,
                 Title = c.Title,
                 Content = c.Content,
                 ContextType = c.ContextType,
                 ContextValue = c.ContextValue,
                 Student = new StudentDTO()
                 {
                     FirstName = c.Student.FirstName,
                     LastName = c.Student.LastName,
                     Program = c.Student.Program.ToString(),
                     YearLevel = c.Student.YearLevel.ToString(),
                     Status = c.Student.Status.ToString(),
                 }
            }).ToListAsync();
            return Ok(confessions);
        }

        [HttpGet("{id:guid}",Name ="GetConfession")]
        public async Task<IActionResult> GetConfession(Guid id)
        {

            var confession = await _context.Confessions.Include(c => c.Student).Where(c=>c.ConfessionId == id).Select(c =>
            new GetAllConfessionDTO()
            {
                StudentId = c.StudentId,
                ContextType = c.ContextType,
                ContextValue = c.ContextValue,
                Content = c.Content,
                Title = c.Title,
                Student = new StudentDTO()
                {
                    FirstName = c.Student.FirstName,
                    LastName = c.Student.LastName,
                    Program = c.Student.Program.ToString(),
                    YearLevel = c.Student.YearLevel.ToString(),
                    Status = c.Student.Status.ToString(),
                }
            
            }).SingleOrDefaultAsync();
            if (confession == null)
            {
                return NotFound();
            }
            return Ok(confession);
        }
        [HttpPost]
        public async Task<IActionResult> CreateConfession(AddConfessionDTO conf)
        {

            // UNCOMMENT MEH IF FIXED NA
            var student = await _context.Students.FindAsync(conf.StudentId);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            Confession newConfession = new Confession()
            {
                StudentId = conf.StudentId,
                Student = student,
                ContextType = conf.ContextType,
                ContextValue = conf.ContextValue.ToString(),
                Title = conf.Title,
                Content = conf.Content,
                CreatedOn = DateTime.UtcNow
            };
            await _context.AddAsync(newConfession);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetConfession),new {id = newConfession.ConfessionId},newConfession);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteConfession(Guid id)
        {
            var confession = await _context.Confessions.FindAsync(id);
            if (confession == null)
            {
                return NotFound("Confession Not Found");
            }
            _context.Confessions.Remove(confession);
            await _context.SaveChangesAsync();
            return Ok("Successfully deleted confession");
        }
        //  di muupdate ang student but tell me lang if nahan muupdate pud ang student
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateConfession(Guid id, UpdateConfessionDTO confDTO)
        {
            //var conf = await _context.Confessions.Select(c => new UpdateConfessionDTO()
            //{

            //    ContextType = c.ContextType,
            //    ContextValue = c.ContextValue,
            //    Content = c.Content,
            //    Title = c.Title,

            //}).SingleOrDefaultAsync();
            var conf = await _context.Confessions.FindAsync(id);
            if (conf == null)
            {
                return NotFound("Confession not found");
            }
            conf.ContextType = confDTO.ContextType;
            conf.ContextValue = confDTO.ContextValue;
            conf.Title = confDTO.Title;
            conf.Content = confDTO.Content;

            await _context.SaveChangesAsync();
            return Ok(conf);
            //return CreatedAtAction(nameof(GetConfession), new { id = conf. }, conf);

        }
    }
}
