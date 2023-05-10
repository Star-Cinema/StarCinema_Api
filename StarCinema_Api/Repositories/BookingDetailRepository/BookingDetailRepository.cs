using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.BookingDetailRepository
{
    public class BookingDetailRepository : BaseRepository<BookingDetail>, IBookingDetailRepository
    {
        public BookingDetailRepository(MyDbContext context) : base(context)
        {
        }
    }
}
