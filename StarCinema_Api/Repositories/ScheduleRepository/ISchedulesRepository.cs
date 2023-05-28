using StarCinema_Api.DTOs;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.ScheduleRepository
{
    /*
        Account : AnhNT282
        Description : Interface repository for entity schedule
        Date created : 2023/05/18
    */
    public interface ISchedulesRepository
    {
        // Get all schedules AnhNT282
        Task<PaginationDTO<Schedules>> getAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int? page, int? limit);
        // Get schedule by id AnhNT282
        Task<Schedules> getScheduleById(int scheduleId);
        // Create schedule AnhNT282
        void CreateSchedule(Schedules schedule);
        // Update schedule AnhNT282
        void UpdateSchedule(Schedules schedule);
        // Delete schedule AnhNT282
        void DeleteSchedule(Schedules schedule);
        // Get id of last schedule AnhNT282
        Task<int> GetLastIDSchedule();
        // Save change DbContext AnhNT282
        bool SaveChange();
    }
}
