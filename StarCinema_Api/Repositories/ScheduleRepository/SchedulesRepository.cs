using StarCinema_Api.DTOs;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.Repositories.ScheduleRepository;
using System.Xml.Schema;
using System.Linq.Dynamic.Core;

namespace StarCinema_Api.Repositories.ScheduleRepository
{

    /// <summary>
    /// Account : AnhNT282
    /// Description : Class repository for entity schedule
    /// Date created : 2023/05/19
    /// </summary>
    public class SchedulesRepository : ISchedulesRepository
    {
        private readonly MyDbContext _context;

        /// <summary>
        /// Constructor AnhNT282
        /// </summary>
        /// <param name="context"></param>
        public SchedulesRepository(MyDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Create schedule AnhNT282
        /// </summary>
        /// <param name="schedule"></param>
        public void CreateSchedule(Schedules schedule)
        {
            _context.Schedules.Add(schedule);
        }

        /// <summary>
        /// Delete schedule AnhNT282
        /// </summary>
        /// <param name="schedule"></param>
        public void DeleteSchedule(Schedules schedule)
        {
            _context.Schedules.Remove(schedule);
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
        public async Task<PaginationDTO<Schedules>> GetAllSchedules(int? filmId, int? roomId, DateTime? date, string? sortDate, int? page, int? limit)
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

        /// <summary>
        /// Get id of last schedule AnhNT282
        /// </summary>
        public async Task<int> GetLastIDSchedule()
        {
            return _context.Schedules.OrderBy(s => s.Id).LastOrDefaultAsync().Result.Id;
        }

        /// <summary>
        /// Get schedule by id AnhNT282
        /// </summary>
        /// <param name="scheduleId"></param>
        public async Task<Schedules> GetScheduleById(int scheduleId)
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

        /// <summary>
        /// Check the booked schedule AnhNT282
        /// </summary>
        /// <param name="schedule"></param>
        public async Task<bool> IsScheduleBooked(Schedules schedule)
        {

            int totalBooking = (from bd in _context.BookingDetails
                         join t in _context.Tickets on bd.TicketId equals t.Id
                         join s in _context.Schedules on t.ScheduleId equals s.Id
                         where s.Id == schedule.Id
                         select new Schedules { Id = s.Id }).ToListAsync().Result.Count();

            return totalBooking > 0;
        }

        /// <summary>
        /// Save change DbContext AnhNT282
        /// </summary>
        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// Update schedule AnhNT282
        /// </summary>
        /// <param name="schedule"></param>
        public void UpdateSchedule(Schedules schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
        }
    }
}
