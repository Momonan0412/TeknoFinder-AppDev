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
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteConfessionAsync(Guid id)
        {
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

        public async Task<GetAllConfessionDTO> GetConfessionByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Confession> UpdateConfessionAsync(Guid id, UpdateConfessionDTO confessionDTO)
        {
            throw new NotImplementedException();
        }
    }
}
