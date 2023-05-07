using booking_my_doctor.DTOs;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.Repositories.ScheduleRepository;
using System.Xml.Schema;

namespace StarCinema_Api.Repositories.ScheduleRepository

{
    public class SchedulesRepository : ISchedulesRepository
    {
        private readonly MyDbContext _context;
        public SchedulesRepository(MyDbContext context)
        {
            _context = context;
        }
        public void CreateSchedule(Schedules schedule)
        {
            _context.Schedules.Add(schedule);
        }

        public void DeleteSchedule(Schedules schedule)
        {
            _context.Schedules.Remove(schedule);
        }

        public async Task<PaginationDTO<Schedules>> getAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int page = 0, int limit = 10)
        {
            var query =  _context.Schedules.Select(x => new Schedules
            {
                Id = x.Id,
                FilmId = x.FilmId,
                RoomId = x.RoomId,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Film = x.Film,
                Room = x.Room,
                Ticket = x.Ticket
            })
            .AsQueryable();
            if(filmId != null)
            {
                query = query.Where(s => s.FilmId == filmId);
            }
            if (roomId != null)
            {
                query = query.Where(s => s.RoomId == roomId);
            }
            if (date != null)
            {
                query = query.Where(s => s.StartTime.Date == date);
            }
            switch(sortDate)
            {
                case "asc":
                    query = query.OrderBy(s => s.StartTime); break;
                case "desc":
                    query = query.OrderByDescending(s => s.StartTime); break;
                default:
                    query = query.OrderByDescending(s => s.StartTime); break;
            }
            var schedules = await query.ToListAsync();
            var pagination = new PaginationDTO<Schedules>();

            pagination.TotalCount = schedules.Count;

            schedules = schedules.Skip(limit*page).Take(limit).ToList();
            pagination.PageSize = limit;
            pagination.Page= page;
            pagination.ListItem = schedules;
            return pagination;
        }

        public async Task<int> GetLastIDSchedule()
        {
            return _context.Schedules.OrderBy(s => s.Id).LastOrDefaultAsync().Result.Id;
        }

        public async Task<Schedules> getScheduleById(int scheduleId)
        {
            return await _context.Schedules.Select(x => new Schedules
            {
                Id = x.Id,
                FilmId = x.FilmId,
                RoomId = x.RoomId,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Film = x.Film,
                Room = x.Room,
                Ticket = x.Ticket
            }).Where(s => s.Id == scheduleId).FirstOrDefaultAsync();
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateSchedule(Schedules schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
        }
    }
}
