using AppDev.API.Data;
using AppDev.API.Interface;
using AppDev.API.Migrations;
using AppDev.API.Models.DataTransferObject.Schedule;
using AppDev.API.Models.Entities;
using AppDev.API.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SchedulesController : ControllerBase
    {
        private readonly ISchedule _scheduleService;

        public SchedulesController(ISchedule scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet("Debugging")]
        public async Task<IActionResult> GetAllSchedulesDebug()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(schedules);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchedules()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(schedules);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetScheduleById(Guid id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
                return NotFound("Schedule not found");
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(ScheduleDTO scheduleDTO)
        {
            var newSchedule = await _scheduleService.CreateScheduleAsync(scheduleDTO);
            return CreatedAtAction(nameof(GetScheduleById), new { id = newSchedule.ScheduleId }, newSchedule);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSchedule(Guid id)
        {
            var success = await _scheduleService.DeleteScheduleAsync(id);
            if (!success)
                return NotFound("Schedule not found");
            return Ok("Successfully deleted the schedule");
        }

        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateSchedule(Guid id, ScheduleDTO scheduleDTO)
        {
            var success = await _scheduleService.UpdateScheduleAsync(id, scheduleDTO);
            if (!success)
                return NotFound("Schedule not found");
            return Ok("Schedule updated successfully");
        }
    }
}
