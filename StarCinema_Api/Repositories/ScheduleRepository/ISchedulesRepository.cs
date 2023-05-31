using StarCinema_Api.DTOs;
using StarCinema_Api.Data.Entities;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace StarCinema_Api.Repositories.ScheduleRepository
{
    /// <summary>
    /// Account : AnhNT282
    /// Description : Interface repository for entity schedule
    /// Date created : 2023/05/18
    /// </summary>
    public interface ISchedulesRepository
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
        Task<PaginationDTO<Schedules>> GetAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int? page, int? limit);
        /// <summary>
        /// Get schedule by id AnhNT282
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        Task<Schedules> GetScheduleById(int scheduleId);
        /// <summary>
        /// Create schedule AnhNT282
        /// </summary>
        /// <param name="schedule"></param>
        void CreateSchedule(Schedules schedule);
        /// <summary>
        /// Update schedule AnhNT282
        /// </summary>
        /// <param name="schedule"></param>
        void UpdateSchedule(Schedules schedule);
        /// <summary>
        /// Delete schedule AnhNT282
        /// </summary>
        /// <param name="schedule"></param>
        void DeleteSchedule(Schedules schedule);
        /// <summary>
        /// Get id of last schedule AnhNT282
        /// </summary>
        Task<int> GetLastIDSchedule();
        /// <summary>
        /// Check the booked schedule AnhNT282
        /// </summary>
        /// <param name="schedule"></param>
        Task<bool> IsScheduleBooked(Schedules schedule);
        /// <summary>
        /// Save change DbContext AnhNT282
        /// </summary>
        bool SaveChange();
    }
}
