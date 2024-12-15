using AppDev.API.Data;
using AppDev.API.Models.DataTransferObject.Confession;
using AppDev.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.DataTransferObject.Schedule;
using AppDev.API.Models.EnumValidation;
using AppDev.API.Interface;
using AppDev.API.Models.Service;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;

namespace AppDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConfessionsController : ControllerBase
    {
        IConfession _confessionService;
        public ConfessionsController(IConfession confessionService) => confessionService = _confessionService ?? throw new ArgumentNullException(nameof(confessionService));
        
        [HttpGet("DebugPurposes")]

        [HttpGet]
        public async Task<IActionResult> GetAllConfessions()
        {
            var confessions = await _confessionService.GetAllConfessionsAsync();
            return Ok(confessions);
        }

        [HttpGet("{id:guid}",Name ="GetConfession")]
        public async Task<IActionResult> GetConfession(Guid id)
        {

            var confession = await _confessionService.GetConfessionByIdAsync(id);
            if (confession == null) return NotFound();
            return Ok(confession);
        }
        [HttpPost]
        public async Task<IActionResult> CreateConfession(AddConfessionDTO confessionDTO)
        {
            try
            {
                var confession = await _confessionService.CreateConfessionAsync(confessionDTO);
                return CreatedAtAction(nameof(GetConfession), new { id = confession.ConfessionId }, confession);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteConfession(Guid id)
        {
            var confession = await _confessionService.DeleteConfessionAsync(id);
            if (confession) return NotFound();
            return Ok("Successfully deleted confession");
        }
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateConfession(Guid id, UpdateConfessionDTO confDTO)
        {
            try
            {
                var updatedConfession = await _confessionService.UpdateConfessionAsync(id, confDTO);
                return Ok(updatedConfession);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
