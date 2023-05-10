using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services
{
    public interface ISchedulesService
    {
        Task<ResponseDTO> GetAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int page = 0, int limit = 10);
        Task<ResponseDTO> GetScheduleById(int id);
        Task<ResponseDTO> CreateSchedule(ScheduleDTO schedule);
        Task<ResponseDTO> UpdateSchedule(int id, ScheduleDTO schedule);
        Task<ResponseDTO> DeleteSchedule(int id);
    }
}
