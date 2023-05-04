using AutoMapper;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.ScheduleRepository;

namespace StarCinema_Api.Services
{
    public class SchedulesService : ISchedulesService
    {
        private readonly ISchedulesRepository _schedulesRepository;
        private readonly IMapper _mapper;
        public SchedulesService(ISchedulesRepository SchedulesRepository,
            IMapper mapper)
        {
            _schedulesRepository = SchedulesRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDTO> CreateSchedule(ScheduleDTO scheduleDTO)
        {
            try
            {
                var schedule = _mapper.Map<ScheduleDTO, Schedules>(scheduleDTO);
                await _schedulesRepository.InsertAsync(schedule);
                _schedulesRepository.Save();
                return new ResponseDTO
                {
                    data = 200,
                    message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> DeleteSchedule(int id)
        {
            try
            {
                var schedule = await _schedulesRepository.GetByIdAsync(id);
                if (schedule == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist schedule with id {id}",
                };
                _schedulesRepository.DeleteAsync(schedule);
                _schedulesRepository.Save();
                return new ResponseDTO
                {
                    data = 200,
                    message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> GetAllSchedules()
        {
            try
            {
                var result = await _schedulesRepository.GetAllAsync();
                var resultDTO = result.Select(_mapper.Map<Schedules, ScheduleDTO>);
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = resultDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> GetScheduleById(int id)
        {
            try
            {
                var result = _schedulesRepository.GetByIdAsync(id);
                if (result == null) return new ResponseDTO
                {
                    code = 400,
                    message = $"Does not exist user with id {id}"
                };
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> UpdateSchedule(int id, ScheduleDTO scheduleDTO)
        {
            try
            {
                var result = _schedulesRepository.GetByIdAsync(id);
                if (result == null) return new ResponseDTO
                {
                    code = 400,
                    message = $"Does not exist user with id {id}"
                };
                var schedule = _mapper.Map<ScheduleDTO, Schedules>(scheduleDTO);
                schedule.Id = id;
                _schedulesRepository.Update(schedule);
                _schedulesRepository.Save();
                return new ResponseDTO { code = 200, message = "Success" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }
        public static bool IsScheduleConflicting(ScheduleDTO newSchedule, List<ScheduleDTO> scheduleList)
        {
            foreach (var schedule in scheduleList)
            {
                if (newSchedule.startTime < schedule.endTime && newSchedule.startTime > schedule.endTime)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
