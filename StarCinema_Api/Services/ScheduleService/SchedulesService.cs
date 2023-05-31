using AutoMapper;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.FilmsRepository;
using StarCinema_Api.Repositories.RoomRepository;
using StarCinema_Api.Repositories.ScheduleRepository;
using StarCinema_Api.Repositories.TicketsRepository;

namespace StarCinema_Api.Services
{
    /// <summary>
    /// Account : AnhNT282
    /// Description : Class service for entity schedule
    /// Date created : 2023/05/19
    /// </summary>
    public class SchedulesService : ISchedulesService
    {
        private readonly ISchedulesRepository _schedulesRepository;
        private readonly IMapper _mapper;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IFilmsRepository _filmsRepository;

        /// <summary>
        /// Constructor AnhNT282
        /// </summary>
        /// <param name="SchedulesRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="ticketsRepository"></param>
        /// <param name="roomRepository"></param>
        /// <param name="filmsRepository"></param>
        public SchedulesService(ISchedulesRepository SchedulesRepository,
            IMapper mapper, ITicketsRepository ticketsRepository,
            IRoomRepository roomRepository,
            IFilmsRepository filmsRepository)
        {
            _schedulesRepository = SchedulesRepository;
            _mapper = mapper;
            _ticketsRepository = ticketsRepository;
            _roomRepository = roomRepository;
            _filmsRepository = filmsRepository;
        }

        /// <summary>
        /// Create schedule AnhNT282
        /// </summary>
        /// <param name="scheduleDTO"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> CreateSchedule(ScheduleDTO scheduleDTO)
        {
            try
            {
                // Check filmID, roomId
                var film = _filmsRepository.GetFilmById(scheduleDTO.FilmId);
                var room = _roomRepository.GetById(scheduleDTO.RoomId);
                Task.WaitAll(film);
                if (film.Result == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist film with id {scheduleDTO.FilmId}"
                };
                if (film.Result.IsDelete == true) return new ResponseDTO
                {
                    code = 400,
                    message = $"The film has been deleted"
                };
                if (room.Result == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist room with id {scheduleDTO.RoomId}"
                };
                if (film.Result.IsDelete == true) return new ResponseDTO
                {
                    code = 400,
                    message = $"The room has been deleted"
                };
                var schedule = _mapper.Map<ScheduleDTO, Schedules>(scheduleDTO);
                schedule.EndTime = schedule.StartTime.AddMinutes(film.Result.Duration);
                var scheduleList = _schedulesRepository.GetAllSchedules(null, schedule.RoomId, schedule.StartTime.Date, null, 0, int.MaxValue).Result.ListItem;
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
                // Create ticket AnhNT282
                await _ticketsRepository.InsertAsync(new Tickets
                {
                    ScheduleId = scheduleId,
                    Price = scheduleDTO.Price,              
                });
                _ticketsRepository.Save();
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

        /// <summary>
        /// Delete schedule AnhNT282
        /// </summary>
        /// <param name="id"></param>
        public async Task<ResponseDTO> DeleteSchedule(int id)
        {
            try
            {
                var schedule = await _schedulesRepository.GetScheduleById(id);
                if (schedule == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist schedule with id {id}",
                };
                // check if the schedule has been booked, it cannot be deleted AnhNT282 
                if (await _schedulesRepository.IsScheduleBooked(schedule))
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = "The booked schedule cannot be deleted"
                    };
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

        /// <summary>
        /// Get all schedules AnhNT282
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="roomId"></param>
        /// <param name="date"></param>
        /// <param name="sortDate"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        public async Task<ResponseDTO> GetAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int? page, int? limit)
        {
            try
            {
                var result = await _schedulesRepository.GetAllSchedules(filmId, roomId, date, sortDate, page, limit);
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

        /// <summary>
        /// Get schedule by id AnhNT282
        /// </summary>
        /// <param name="id"></param>
        public async Task<ResponseDTO> GetScheduleById(int id)
        {
            try
            {
                var result = await _schedulesRepository.GetScheduleById(id);
                if (result == null) return new ResponseDTO
                {
                    code = 404,
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

        /// <summary>
        /// Update schedule AnhNT282
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scheduleDTO"></param>
        public async Task<ResponseDTO> UpdateSchedule(int id, ScheduleDTO scheduleDTO)
        {
            try
            {
                // Check filmId, roomId
                var film = _filmsRepository.GetFilmById(scheduleDTO.FilmId);
                var room = _roomRepository.GetById(scheduleDTO.RoomId);
                var scheduleCurrent = _schedulesRepository.GetScheduleById(id);

                Task.WaitAll(room, film, scheduleCurrent);
                if (film.Result == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist film with id {scheduleDTO.FilmId}"
                };
                if (room.Result == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist room with id {scheduleDTO.RoomId}"
                };
                if (scheduleCurrent.Result == null) return new ResponseDTO
                {
                    code = 404,
                    message = $"Does not exist schedule with id {id}"
                };
                var schedule = _mapper.Map<ScheduleDTO, Schedules>(scheduleDTO);
                schedule.EndTime = schedule.StartTime.AddMinutes(film.Result.Duration);




                var scheduleNew = _mapper.Map<ScheduleDTO, Schedules>(scheduleDTO);
                scheduleNew.Id = id;
                scheduleNew.EndTime = scheduleNew.StartTime.AddMinutes(film.Result.Duration); 

                var scheduleList = _schedulesRepository.GetAllSchedules(null, scheduleNew.RoomId, scheduleNew.StartTime.Date, null, 0, int.MaxValue).Result.ListItem;

                scheduleList = scheduleList.Where(s => s.Id != scheduleCurrent.Result.Id).ToList();
                var isInvalid = IsScheduleConflicting(scheduleNew, scheduleList);
                if(isInvalid)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"The time is the same as the show time of another film"
                    };
                }
                scheduleCurrent.Result.Ticket.Price = scheduleDTO.Price;
                _schedulesRepository.UpdateSchedule(scheduleNew);

                _ticketsRepository.Save();
                _schedulesRepository.SaveChange();
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
        /// <summary>
        /// Check for overlap with previous schedules AnhNT282
        /// </summary>
        /// <param name="newSchedule"></param>
        /// <param name="scheduleList"></param>
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
