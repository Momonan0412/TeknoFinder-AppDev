using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.Entities;
using AppDev.API.Models.EnumValidation;

namespace AppDev.API.Models.Mapper
{
    public class StudentMapper
    {
        // Convert Student entity to StudentDTO
        public static StudentDTO ConvertToDTO(Student student)
        {
            return new StudentDTO
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                StudentNumber = student.StudentNumber,
                Program = student.Program.ToString(),
                YearLevel = student.YearLevel.ToString(),
                Status = student.Status.ToString()
            };
        }

        // Convert StudentDTO to Student entity
        public static Student ConvertFromDTO(StudentDTO dto)
        {   
            return new Student
            {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            StudentNumber = dto.StudentNumber,
            Program = Enum.Parse<StudentProgram>(dto.Program),
            YearLevel = Enum.Parse<YearLevel>(dto.YearLevel),
            Status = Enum.Parse<Status>(dto.Status)
            };
        }
    }
}
