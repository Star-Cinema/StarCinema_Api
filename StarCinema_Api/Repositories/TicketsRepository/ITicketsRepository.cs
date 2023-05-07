using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.TicketsRepository
{
    public interface ITicketsRepository : IBaseRepository<Tickets>
    {
        Task<int> GetLastIDTicket();
    }
}
