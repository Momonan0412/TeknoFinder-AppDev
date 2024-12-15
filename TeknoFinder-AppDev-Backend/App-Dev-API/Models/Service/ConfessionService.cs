using AppDev.API.Data;
using AppDev.API.Interface;
using AppDev.API.Models.DataTransferObject.Confession;
using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDev.API.Models.Service
{
    public class ConfessionService : IConfession
    {
        ApplicationDbContext _applicationDbContext;
        public ConfessionService(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public async Task<Confession> CreateConfessionAsync(AddConfessionDTO confessionDTO)
        {
            // Find the student by ID
            var student = await _applicationDbContext.Students
                .Include(s => s.Confessions) // Load existing Confessions if needed
                .FirstOrDefaultAsync(s => s.StudentIdentification == confessionDTO.StudentId);

            if (student == null)
            {
                throw new Exception("Student not found");
            }

            // Create the new confession
            var newConfession = new Confession
            {
                StudentId = student.StudentIdentification, // Only set StudentId; EF will resolve the relationship
                ContextType = confessionDTO.ContextType,
                ContextValue = confessionDTO.ContextValue,
                Title = confessionDTO.Title,
                Content = confessionDTO.Content,
                CreatedOn = DateTime.UtcNow
            };

            // Add the new confession
            student.Confessions.Add(newConfession); // Update navigation property explicitly if needed
            await _applicationDbContext.SaveChangesAsync();

            return newConfession;
        }


        public async Task<bool> DeleteConfessionAsync(Guid id)
        {
            var confession = await _applicationDbContext.Confessions.FindAsync(id);
            if (confession == null) { return false; }
            _applicationDbContext.Confessions.Remove(confession);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetAllConfessionDTO>> GetAllConfessionsAsync()
        {
            return await _applicationDbContext.Confessions
                .Include(c => c.Student)
                .Select(c => new GetAllConfessionDTO
                {
                    StudentId = c.StudentId,
                    Title = c.Title,
                    Content = c.Content,
                    ContextType = c.ContextType,
                    ContextValue = c.ContextValue,
                    Student = new StudentDTO
                    {
                        FirstName = c.Student.FirstName,
                        LastName = c.Student.LastName,
                        Program = c.Student.Program.ToString(),
                        YearLevel = c.Student.YearLevel.ToString(),
                        Status = c.Student.Status.ToString(),
                    }
                }).ToListAsync();
        }

        public async Task<GetAllConfessionDTO?> GetConfessionByIdAsync(Guid id)
        {
            return await _applicationDbContext.Confessions
                .Include(c => c.Student)
                .Where(c => c.ConfessionId == id)
                .Select(c => new GetAllConfessionDTO
                {
                    StudentId = c.StudentId,
                    Title = c.Title,
                    Content = c.Content,
                    ContextType = c.ContextType,
                    ContextValue = c.ContextValue,
                    Student = new StudentDTO
                    {
                        FirstName = c.Student.FirstName,
                        LastName = c.Student.LastName,
                        Program = c.Student.Program.ToString(),
                        YearLevel = c.Student.YearLevel.ToString(),
                        Status = c.Student.Status.ToString(),
                    }
                }).SingleOrDefaultAsync();
        }

        public async Task<Confession> UpdateConfessionAsync(Guid id, UpdateConfessionDTO confessionDTO)
        {
            var confession = await _applicationDbContext.Confessions.FindAsync(id);
            if (confession == null) { return null;}
            confession.ContextType = confessionDTO.ContextType;
            confession.ContextValue = confessionDTO.ContextValue;
            confession.Title = confessionDTO.Title;
            confession.Content = confessionDTO.Content;
            await _applicationDbContext.SaveChangesAsync();
            return confession;
        }
    }
}
