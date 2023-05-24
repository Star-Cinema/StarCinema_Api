using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.TicketsRepository
{
    /*
        Account : AnhNT282
        Description : Interface repository for entity ticket
        Date created : 2023/05/05
    */
    public interface ITicketsRepository : IBaseRepository<Tickets>
    {
        // Get id of last ticket AnhNT282
        Task<int> GetLastIDTicket();
    }
}
