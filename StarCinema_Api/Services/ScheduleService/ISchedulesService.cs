using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services
{
    /// <summary>
    /// Account : AnhNT282
    /// Description : Interface service for entity schedule
    /// Date created : 2023/05/19
    /// </summary>
    public interface ISchedulesService
    {
        /// <summary>
        /// Get all schedules AnhNT282
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="roomId"></param>
        /// <param name="date"></param>
        /// <param name="sortDate"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<ResponseDTO> GetAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int? page, int? limit);
        /// <summary>
        /// Get schedule by id AnhNT282
        /// </summary>
        /// <param name="id"></param>
        Task<ResponseDTO> GetScheduleById(int id);
        /// <summary>
        /// Create schedule AnhNT282
        /// </summary>
        /// <param name="schedule"></param>
        Task<ResponseDTO> CreateSchedule(ScheduleDTO schedule);
        /// <summary>
        /// Update schedule AnhNT282
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schedule"></param>
        Task<ResponseDTO> UpdateSchedule(int id, ScheduleDTO schedule);
        /// <summary>
        /// Delete schedule AnhNT282
        /// </summary>
        /// <param name="id"></param>
        Task<ResponseDTO> DeleteSchedule(int id);
    }
}
