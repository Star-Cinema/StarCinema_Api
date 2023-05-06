using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.Repositories.ScheduleRepository;

namespace StarCinema_Api.Repositories.ScheduleRepository

{
    public class SchedulesRepository : BaseRepository<Schedules>, ISchedulesRepository
    {
        public SchedulesRepository(MyDbContext context) : base(context)
        {
        }
    }
}
