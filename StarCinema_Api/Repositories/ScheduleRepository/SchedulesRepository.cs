using StarCinema_Api.DTOs;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.Repositories.ScheduleRepository;
using System.Xml.Schema;

namespace StarCinema_Api.Repositories.ScheduleRepository
{
    /*
        Account : AnhNT282
        Description : Class repository for entity schedule
        Date created : 2023/05/19
    */
    public class SchedulesRepository : ISchedulesRepository
    {
        private readonly MyDbContext _context;

        // Constructor AnhNT282
        public SchedulesRepository(MyDbContext context)
        {
            _context = context;
        }
        // Create schedule AnhNT282
        public void CreateSchedule(Schedules schedule)
        {
            _context.Schedules.Add(schedule);
        }

        // Delete schedule AnhNT282
        public void DeleteSchedule(Schedules schedule)
        {
            _context.Schedules.Remove(schedule);
        }

        // Get all schedules AnhNT282
        public async Task<PaginationDTO<Schedules>> getAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int? page, int? limit)
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

            if (page != null || limit != null) {
                schedules = schedules.Skip(limit.Value * page.Value).Take(limit.Value).ToList();
                pagination.PageSize = limit.Value;
                pagination.Page = page.Value;
            } else
            {
                pagination.PageSize = int.MaxValue;
                pagination.Page = 0;
            }

            pagination.ListItem = schedules;
            return pagination;
        }

        // Get id of last schedule AnhNT282
        public async Task<int> GetLastIDSchedule()
        {
            return _context.Schedules.OrderBy(s => s.Id).LastOrDefaultAsync().Result.Id;
        }

        // Get schedule by id AnhNT282
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

        // Save change DbContext AnhNT282
        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }

        // Update schedule AnhNT282
        public void UpdateSchedule(Schedules schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
        }
    }
}
