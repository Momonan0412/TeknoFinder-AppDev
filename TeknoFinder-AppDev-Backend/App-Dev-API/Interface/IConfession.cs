using AppDev.API.Models.DataTransferObject.Confession;
using AppDev.API.Models.Entities;

namespace AppDev.API.Interface
{
    public interface IConfession
    {
        Task<List<GetAllConfessionDTO>> GetAllConfessionsAsync();
        Task<GetAllConfessionDTO> GetConfessionByIdAsync(Guid id);
        Task<Confession> CreateConfessionAsync(AddConfessionDTO confessionDTO);
        Task<bool> DeleteConfessionAsync(Guid id);
        Task<Confession> UpdateConfessionAsync(Guid id, UpdateConfessionDTO confessionDTO);
    }
}
