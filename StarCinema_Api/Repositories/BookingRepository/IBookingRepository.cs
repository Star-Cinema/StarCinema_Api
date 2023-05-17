using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Repositories.BookingRepository
{
    public interface IBookingRepository : IBaseRepository<Bookings>
    {
        Task<PaginationDTO<BookingDTO>> GetAllBookings(int page, int pageSize);

        Task<BookingDTO> GetDetailBookingById(int id);

        Task<ResponseDTO> CreateBooking(BookingDTO bookingDTO);

        void DeleteBooking(Bookings bookings);

    }
}
