using AppDev.API.Data;
using AppDev.API.Interface;
using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.DataTransferObject.User;
using AppDev.API.Models.DataTransferObject.UserAndStudent;
using AppDev.API.Models.Entities;
using AppDev.API.Models.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppDev.API.Models.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<(bool Success, string? ErrorMessage, User user, Student student)> AddUserAndStudentAsync(UserAndStudentDTO userAndStudentDTO) { 
            using var transaction = await applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                // Debugging Step
                Debug.WriteLine($"UserDTO: {userAndStudentDTO.AddUserDTO.ToString()}");
                Debug.WriteLine($"StudentDTO: {userAndStudentDTO.StudentDTO.ToString()}");

                var newStudent = StudentMapper.ConvertFromDTO(userAndStudentDTO.StudentDTO);
                applicationDbContext.Students.Add(newStudent);
                await applicationDbContext.SaveChangesAsync();

                var newUser = UserMapper.ConvertFromDTO(userAndStudentDTO.AddUserDTO);
                newUser.StudentIdentification = newStudent.StudentIdentification;
                applicationDbContext.Users.Add(newUser);
                await applicationDbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return (true, null, newUser, newStudent);
            }
            catch (DbUpdateException dbEx)
            {
                // Log the detailed error information for troubleshooting
                var errorMessage = $"An error occurred while updating the database: {dbEx.Message}";
                if (dbEx.InnerException != null)
                {
                    errorMessage += $"\nInner Exception: {dbEx.InnerException.Message}";
                }

                await transaction.RollbackAsync();
                return (false, errorMessage, new User(), new Student());
            }
            catch (Exception ex)
            {
                // General exception catch block for unexpected errors
                await transaction.RollbackAsync();
                return (false, ex.Message, new User(), new Student());
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await applicationDbContext.Users.ToListAsync();
        }

        public async Task<User?> UpdateUserAsync(Guid id, UpdateUserDTO updateUserDTO)
        {

            var user = await applicationDbContext.Users.FindAsync(id);
            if (user is null) return null;
            if (!string.IsNullOrEmpty(updateUserDTO.Email))
            {
                user.Email = updateUserDTO.Email;
            }
            if (!string.IsNullOrEmpty(updateUserDTO.Password))
            {
                user.Password = updateUserDTO.Password;
            }
            if (user.IsActive != updateUserDTO.IsActive)
            {
                user.IsActive = updateUserDTO.IsActive;
            }
            await applicationDbContext.SaveChangesAsync();
            return user;
        }
        public async Task<User?> ToggleUserIsActiveAsync(Guid id)
        {
            var user = await applicationDbContext.Users.FindAsync(id);
            if (user is null) return null;

            user.IsActive = !user.IsActive;
            await applicationDbContext.SaveChangesAsync();
            return user;
        }
    }
}
