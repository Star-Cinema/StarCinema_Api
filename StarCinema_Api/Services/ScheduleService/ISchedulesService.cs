using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services
{
    /*
        Account : AnhNT282
        Description : Interface service for entity schedule
        Date created : 2023/05/19
    */
    public interface ISchedulesService
    {
        // Get all schedules AnhNT282
        Task<ResponseDTO> GetAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int? page, int? limit);
        // Get schedule by id AnhNT282
        Task<ResponseDTO> GetScheduleById(int id);
        // Create schedule AnhNT282
        Task<ResponseDTO> CreateSchedule(ScheduleDTO schedule);
        // Update schedule AnhNT282
        Task<ResponseDTO> UpdateSchedule(int id, ScheduleDTO schedule);
        // Delete schedule AnhNT282
        Task<ResponseDTO> DeleteSchedule(int id);
    }
}
