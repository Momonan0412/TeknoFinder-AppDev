using AppDev.API.Data;
using AppDev.API.Interface;
using AppDev.API.Models.DataTransferObject.Confession;
using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.Entities;
using AppDev.API.Models.EnumValidation;
using Microsoft.EntityFrameworkCore;

namespace AppDev.API.Models.Service
{
    public class ConfessionService : IConfession
    {
        ApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public ConfessionService(ApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService
            ) => (_applicationDbContext, _currentUserService) = (applicationDbContext, currentUserService);

        public async Task<Confession> CreateConfessionAsync(AddConfessionDTO confessionDTO)
        {
            // Find the student by ID
            var studentID = _currentUserService.StudentId;
            var student = await _applicationDbContext.Students.FindAsync( studentID );

            if (student == null)
            {
                throw new Exception("Student not found");
            }
            // Initialize the Confessions collection if it is null
            if (student.Confessions == null)
            {
                student.Confessions = new List<Confession>();
            }
            // Create the new confession
            var newConfession = new Confession
            {
                StudentId = student.StudentIdentification, // Only set StudentId; EF will resolve the relationship
                ContextType = Enum.Parse<ConfessionContextType>(confessionDTO.ContextType),
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
            var user = await _applicationDbContext.Users.FindAsync(_currentUserService.UserId);
            if (user is null) return null;
            return await _applicationDbContext.Confessions
                .Include(c => c.Student)
                .Select(c => new GetAllConfessionDTO
                {
                    StudentId = c.StudentId,
                    Title = c.Title,
                    Content = c.Content,
                    ContextType = c.ContextType.ToString(),
                    ContextValue = c.ContextValue,
                    CreatedOn = c.CreatedOn,
                    Student = new ConfessionStudentDTO
                    {
                        FirstName = c.Student.FirstName,
                        LastName = c.Student.LastName,
                        Program = c.Student.Program.ToString(),
                        YearLevel = c.Student.YearLevel.ToString(),
                        Status = c.Student.Status.ToString(),
                        Username = user.Username
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
                    ContextType = c.ContextType.ToString(),
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
