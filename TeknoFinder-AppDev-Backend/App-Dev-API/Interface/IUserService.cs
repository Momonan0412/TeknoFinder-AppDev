using AppDev.API.Models.DataTransferObject.User;
using AppDev.API.Models.Entities;

namespace AppDev.API.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> UpdateUserAsync(Guid id, UpdateUserDTO updateUserDTO);
        Task<User?> ToggleUserIsActiveAsync(Guid id);
    }
}
