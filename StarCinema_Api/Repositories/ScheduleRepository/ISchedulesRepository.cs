using booking_my_doctor.DTOs;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.ScheduleRepository
{
    public interface ISchedulesRepository
    {
        Task<PaginationDTO<Schedules>> getAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int page = 0, int limit = 10);
        Task<Schedules> getScheduleById(int scheduleId);
        void CreateSchedule(Schedules schedule);
        void UpdateSchedule(Schedules schedule);
        void DeleteSchedule(Schedules schedule);
        Task<int> GetLastIDSchedule();
        bool SaveChange();
    }
}
