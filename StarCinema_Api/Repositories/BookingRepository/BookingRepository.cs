using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Repositories.BookingRepository
{
    public class BookingRepository : BaseRepository<Bookings>, IBookingRepository
    {
        public BookingRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<ResponseDTO> CreateBooking(BookingDTO bookingDTO)
        {
            return new ResponseDTO
            {

            };

        }
    }
}
