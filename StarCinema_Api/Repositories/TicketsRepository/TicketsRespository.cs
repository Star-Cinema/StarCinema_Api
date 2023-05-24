using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.TicketsRepository
{
    /*
        Account : AnhNT282
        Description : Class repository for entity ticket
        Date created : 2023/05/05
    */
    public class TicketsRespository : BaseRepository<Tickets>, ITicketsRepository
    {
        private readonly MyDbContext _context;
        // Constructor AnhNT282
        public TicketsRespository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        // Get id of last ticket AnhNT282
        public async Task<int> GetLastIDTicket()
        {
            return _context.Tickets.OrderBy(t => t.Id).LastOrDefaultAsync().Result.Id;
        }
    }
}
