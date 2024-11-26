using AppDev.API.Handler;
using AppDev.API.Models.DataTransferObject.Student;
using AppDev.API.Models.DataTransferObject.User;
using AppDev.API.Models.DataTransferObject.UserAndStudent;
using AppDev.API.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;

namespace AppDev.API.Models.Mapper
{
    public class UserMapper
    {
        public static UserDTO ConvertToDTO(User user)
        {
            return new UserDTO
            {
                Email = user.Email,
                Password = user.Password,
                IsActive = user.IsActive,
            };
        }

        public static User ConvertFromDTO(UserDTO dto)
        {
            Debug.WriteLine($"UserDTO in Mapper: {dto.ToString()}");
            return new User
            {
                Email = dto.Email,
                Password = dto.Password,
                IsActive = dto.IsActive,
            };
        }
        public static User ConvertFromDTO(AddUserDTO dto)
        {
            Debug.WriteLine($"UserDTO in Mapper: {dto.ToString()}");
            // Hash the password before storing it
            //Debug.WriteLine($"This is Password: {dto.Password}");
            string hashedPassword = PasswordHashHandler.Hash(dto.Password);
            //Debug.WriteLine($"This is the Hashed Password: {hashedPassword}");
            return new User
            {
                Email = dto.Email,
                Password = hashedPassword,
                IsActive = dto.IsActive,
            };
        }
    }
}
