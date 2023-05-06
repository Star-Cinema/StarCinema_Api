using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services
{
    public interface ISchedulesService
    {
        Task<ResponseDTO> GetAllSchedules();
        Task<ResponseDTO> GetScheduleById(int id);
        Task<ResponseDTO> CreateSchedule(ScheduleDTO schedule);
        Task<ResponseDTO> UpdateSchedule(int id, ScheduleDTO schedule);
        Task<ResponseDTO> DeleteSchedule(int id);
    }
}
