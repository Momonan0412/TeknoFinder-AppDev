using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.Entities;

namespace AppDev.API.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO?> GetStudentByIdAsync(Guid id);
        Task<bool> UpdateStudentAsync(Guid id, UpdateStudentDTO updateStudentDTO);
    }
}
