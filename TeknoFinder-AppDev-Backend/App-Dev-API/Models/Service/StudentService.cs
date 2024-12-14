using AppDev.API.Data;
using AppDev.API.Interface;
using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.Entities;
using AppDev.API.Models.Mapper;
using Microsoft.EntityFrameworkCore;

namespace AppDev.API.Models.Service
{
    public class StudentService : IStudentService
    {
        private ApplicationDbContext _applicationDbContext;

        public StudentService(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;
        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _applicationDbContext.Students.ToListAsync();
            return students.Select(student => StudentMapper.ConvertToDTO(student)).ToList();
        }

        public async Task<StudentDTO?> GetStudentByIdAsync(Guid id)
        {
            var student = await _applicationDbContext.Students.FindAsync(id);
            return student is null ? null : StudentMapper.ConvertToDTO(student);
        }

        public async Task<bool> UpdateStudentAsync(Guid id, UpdateStudentDTO updateStudentDTO)
        {
            var student = await _applicationDbContext.Students.FindAsync(id);
            if (student is null)
            {
                return false;
            }

            var updatedStudentInfo = StudentMapper.ConvertFromDTO(updateStudentDTO);
            if (updatedStudentInfo is null)
            {
                throw new ArgumentException("Invalid student DTO");
            }

            // Update fields only if they are not null or empty
            if (!string.IsNullOrEmpty(updatedStudentInfo.FirstName))
            {
                student.FirstName = updatedStudentInfo.FirstName;
            }

            if (!string.IsNullOrEmpty(updatedStudentInfo.LastName))
            {
                student.LastName = updatedStudentInfo.LastName;
            }

            student.Program = updatedStudentInfo.Program;
            student.YearLevel = updatedStudentInfo.YearLevel;
            student.Status = updatedStudentInfo.Status;

            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
