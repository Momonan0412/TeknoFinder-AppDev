using AppDev.API.Data;
using AppDev.API.Migrations;
using AppDev.API.Models.DataTransferObject.Schedule;
using AppDev.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Debugging")]
        public async Task<IActionResult> GetAllSchedulesDebug()
        {
            var scedules = await _context.Schedules.ToListAsync();
            return Ok(scedules);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSchedules()
        {
            var scedules = await _context.Schedules.Select(s=> new ScheduleDTO()
            {
                SubjectTitle = s.SubjectTitle,
                Section = s.Section,
                Classroom = s.Classroom,
                Day = s.Day,
                StartsAt = s.StartsAt,
                EndsAt = s.EndsAt
            }).
            ToListAsync();
            return Ok(scedules);
        }
        [HttpGet("{id:guid}")]
        public async Task <IActionResult> GetScheduleById(Guid id)
        {
            //var sched = await _context.Schedules.Select(s=> new ScheduleDTO()
            //{
            //    SubjectTitle = s.SubjectTitle,
            //    Section = s.Section,
            //    Classroom = s.Classroom,
            //    Day = s.Day,
            //    StartsAt = s.StartsAt,
            //    EndsAt = s.EndsAt
            //}).
            //SingleOrDefaultAsync();
            var sched = await _context.Schedules.FindAsync(id);
            if(sched == null)
            {
                return NotFound("Schedule is not found");
            }
            return Ok(sched);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSchedule(ScheduleDTO scheduleDTO)
        {
            Schedule newSched = new Schedule()
            {
                SubjectTitle = scheduleDTO.SubjectTitle,
                Section = scheduleDTO.Section,
                Classroom = scheduleDTO.Classroom,
                Day = scheduleDTO.Day,
                StartsAt = scheduleDTO.StartsAt,
                EndsAt = scheduleDTO.EndsAt,
            };
            _context.Schedules.Add(newSched);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetScheduleById), new { id = newSched.ScheduleId }, newSched);

        }
        [HttpDelete("{id:guid}")]
        public async Task <IActionResult> DeleteSchedule(Guid id)
        {
            var sched = await _context.Schedules.FindAsync(id);
            if (sched == null)
            {
                return NotFound("Schedule not found");
            }
            _context.Schedules.Remove(sched);
            await _context.SaveChangesAsync();
            return Ok("Successfully deleted the schedule");
        }
        [HttpPut("update/{id:guid}")]
        public async Task <IActionResult> UpdateSchedule(Guid id, ScheduleDTO schedDTO)
        {
            var sched = await _context.Schedules.FindAsync(id);
            if(sched == null)
            {
                return NotFound("Schedule not found");
            }
            sched.SubjectTitle = schedDTO.SubjectTitle;
            sched.Section = schedDTO.Section;
            sched.Classroom = schedDTO.Classroom;
            sched.Day = schedDTO.Day;
            sched.StartsAt = schedDTO.StartsAt;
            sched.EndsAt = schedDTO.EndsAt;

            await _context.SaveChangesAsync();
            return Ok(sched);
        }

        
    }
}
