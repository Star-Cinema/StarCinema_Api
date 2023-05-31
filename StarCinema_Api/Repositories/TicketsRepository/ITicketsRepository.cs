using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.TicketsRepository
{
    /// <summary>
    /// Account : AnhNT282
    /// Description : Interface repository for entity ticket
    /// Date created : 2023/05/05
    /// </summary>
    public interface ITicketsRepository : IBaseRepository<Tickets>
    {
        /// <summary>
        /// Get id of last ticket AnhNT282
        /// </summary>
        Task<int> GetLastIDTicket();
    }
}
