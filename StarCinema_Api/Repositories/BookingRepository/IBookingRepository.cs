using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Repositories.BookingRepository
{
    public interface IBookingRepository : IBaseRepository<Bookings>
    {
        Task<ResponseDTO> CreateBooking(BookingDTO bookingDTO);

    }
}
