using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.TicketsRepository
{
    /// <summary>
    /// Account : AnhNT282
    /// Description : Class repository for entity ticket
    /// Date created : 2023/05/05
    /// </summary>
    public class TicketsRespository : BaseRepository<Tickets>, ITicketsRepository
    {
        private readonly MyDbContext _context;
        /// <summary>
        /// Constructor AnhNT282
        /// </summary>
        /// <param name="context"></param>
        public TicketsRespository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get id of last ticket AnhNT282
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetLastIDTicket()
        {
            return _context.Tickets.OrderBy(t => t.Id).LastOrDefaultAsync().Result.Id;
        }
    }
}
