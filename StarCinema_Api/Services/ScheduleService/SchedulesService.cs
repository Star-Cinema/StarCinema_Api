using AutoMapper;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.ScheduleRepository;
using StarCinema_Api.Repositories.TicketsRepository;

namespace StarCinema_Api.Services
{
    public class SchedulesService : ISchedulesService
    {
        private readonly ISchedulesRepository _schedulesRepository;
        private readonly IMapper _mapper;
        private readonly ITicketsRepository _ticketsRepository;
        public SchedulesService(ISchedulesRepository SchedulesRepository,
            IMapper mapper, ITicketsRepository ticketsRepository)
        {
            _schedulesRepository = SchedulesRepository;
            _mapper = mapper;
            _ticketsRepository = ticketsRepository;
        }
        public async Task<ResponseDTO> CreateSchedule(ScheduleDTO scheduleDTO)
        {
            try
            {
                //if(scheduleDTO.StartTime > scheduleDTO.EndTime)
                //{
                //    return new ResponseDTO
                //    {
                //        code = 400,
                //        message = "The start time must be less than the end time"
                //    };
                //}

                var schedule = _mapper.Map<ScheduleDTO, Schedules>(scheduleDTO);
                schedule.EndTime = schedule.StartTime.AddMinutes(120); // Giả sử cộng thời lượng phim
                var scheduleList = _schedulesRepository.getAllSchedules(null, schedule.RoomId, schedule.StartTime.Date, null, 0, int.MaxValue).Result.ListItem;
                var IsInvalid = IsScheduleConflicting(schedule, scheduleList);
                if (IsInvalid)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"The time is the same as the show time of another film"
                    };
                }

                _schedulesRepository.CreateSchedule(schedule);
                _schedulesRepository.SaveChange();

                var scheduleId = await _schedulesRepository.GetLastIDSchedule();
                //Create ticket
                await _ticketsRepository.InsertAsync(new Tickets
                {
                    ScheduleId = scheduleId,
                    Price = scheduleDTO.Price,
                    
                });
                _schedulesRepository.SaveChange();
                return new ResponseDTO
                {
                    code = 200,
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
                var schedule = await _schedulesRepository.getScheduleById(id);
                if (schedule == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist schedule with id {id}",
                };

                _schedulesRepository.DeleteSchedule(schedule);
                _schedulesRepository.SaveChange();
                return new ResponseDTO
                {
                    code = 200,
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

        public async Task<ResponseDTO> GetAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int page = 0, int limit = 10)
        {
            try
            {
                var result = await _schedulesRepository.getAllSchedules(filmId, roomId, date, sortDate, page, limit);
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

        public async Task<ResponseDTO> GetScheduleById(int id)
        {
            try
            {
                var result = await _schedulesRepository.getScheduleById(id);
                if (result == null) return new ResponseDTO
                {
                    code = 400,
                    message = $"Does not exist schedule with id {id}"
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

                //if (scheduleDTO.StartTime > scheduleDTO.EndTime)
                //{
                //    return new ResponseDTO
                //    {
                //        code = 400,
                //        message = "The start time must be less than the end time"
                //    };
                //}

                var scheduleCurrent = await _schedulesRepository.getScheduleById(id);
                if (scheduleCurrent == null) return new ResponseDTO
                {
                    code = 400,
                    message = $"Does not exist schedule with id {id}"
                };


                var scheduleNew = _mapper.Map<ScheduleDTO, Schedules>(scheduleDTO);
                scheduleNew.Id = id;
                scheduleNew.EndTime = scheduleNew.StartTime.AddMinutes(120); // Giả sử cộng thời lượng phim

                var scheduleList = _schedulesRepository.getAllSchedules(null, scheduleCurrent.RoomId, scheduleCurrent.StartTime.Date, null, 0, int.MaxValue).Result.ListItem;

                scheduleList = scheduleList.Where(s => s.Id != scheduleCurrent.Id).ToList();
                var IsInvalid = IsScheduleConflicting(scheduleNew, scheduleList);
                if(IsInvalid)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"The time is the same as the show time of another film"
                    };
                }
                scheduleCurrent.Ticket.Price = scheduleDTO.Price;
                _schedulesRepository.UpdateSchedule(scheduleNew);

                _ticketsRepository.Save();
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
        public bool IsScheduleConflicting(Schedules newSchedule, List<Schedules> scheduleList)
        {
            if(scheduleList.Count ==0) return false;
            foreach (var schedule in scheduleList)
            {
                if (newSchedule.StartTime < schedule.EndTime && newSchedule.EndTime > schedule.StartTime)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
