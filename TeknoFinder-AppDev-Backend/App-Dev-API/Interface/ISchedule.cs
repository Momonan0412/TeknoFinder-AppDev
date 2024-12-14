using AppDev.API.Models.DataTransferObject.Schedule;
using AppDev.API.Models.Entities;

namespace AppDev.API.Interface
{
    public interface ISchedule
    {
        Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync();
        Task<ScheduleDTO?> GetScheduleByIdAsync(Guid id);
        Task<Schedule> CreateScheduleAsync(ScheduleDTO scheduleDTO);
        Task<bool> UpdateScheduleAsync(Guid id, ScheduleDTO scheduleDTO);
        Task<bool> DeleteScheduleAsync(Guid id);
    }
}
