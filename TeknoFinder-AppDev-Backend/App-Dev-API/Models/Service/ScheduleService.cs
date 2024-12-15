using AppDev.API.Data;
using AppDev.API.Interface;
using AppDev.API.Models.DataTransferObject.Schedule;
using AppDev.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDev.API.Models.Service
{
    public class ScheduleService : ISchedule
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ScheduleService(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;
        public async Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync()
        {
            var schedules = await _applicationDbContext.Schedules.Select(s => new ScheduleDTO
            {
                SubjectTitle = s.SubjectTitle,
                Section = s.Section,
                Classroom = s.Classroom,
                Day = s.Day,
                StartsAt = s.StartsAt,
                EndsAt = s.EndsAt
            }).ToListAsync();
            return schedules;
        }


        public async Task<ScheduleDTO?> GetScheduleByIdAsync(Guid id)
        {
            var schedule = await _applicationDbContext.Schedules
                .Where(s => s.ScheduleId == id)
                .Select(s => new ScheduleDTO
                {
                    SubjectTitle = s.SubjectTitle,
                    Section = s.Section,
                    Classroom = s.Classroom,
                    Day = s.Day,
                    StartsAt = s.StartsAt,
                    EndsAt = s.EndsAt
                })
                .SingleOrDefaultAsync();

            return schedule;
        }
        public async Task<Schedule> CreateScheduleAsync(ScheduleDTO scheduleDTO)
        {
            var newSchedule = new Schedule
            {
                SubjectTitle = scheduleDTO.SubjectTitle,
                Section = scheduleDTO.Section,
                Classroom = scheduleDTO.Classroom,
                Day = scheduleDTO.Day,
                StartsAt = scheduleDTO.StartsAt,
                EndsAt = scheduleDTO.EndsAt
            };
            _applicationDbContext.Schedules.Add(newSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return newSchedule;
        }

        public async Task<bool> UpdateScheduleAsync(Guid id, ScheduleDTO scheduleDTO)
        {
            var schedule = await _applicationDbContext.Schedules.FindAsync(id);
            if (schedule == null)
                return false;

            schedule.SubjectTitle = scheduleDTO.SubjectTitle;
            schedule.Section = scheduleDTO.Section;
            schedule.Classroom = scheduleDTO.Classroom;
            schedule.Day = scheduleDTO.Day;
            schedule.StartsAt = scheduleDTO.StartsAt;
            schedule.EndsAt = scheduleDTO.EndsAt;

            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteScheduleAsync(Guid id)
        {
            var schedule = await _applicationDbContext.Schedules.FindAsync(id);
            if (schedule == null)
                return false;

            _applicationDbContext.Schedules.Remove(schedule);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
