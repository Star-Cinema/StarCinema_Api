using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Repositories.BookingRepository
{
    public interface IBookingRepository : IBaseRepository<Bookings>
    {
        // Admin: Get all films to choose film when create booking
        Task<List<Films>> GetAllFilms();

        // Get all Seats not booked 
        Task<List<Seats>> GetSeatsNotBooked(int filmId, int scheduleId);

        // Get all booking by page, pageSize 
        Task<PaginationDTO<BookingDTO>> GetAllBookings(int page, int pageSize);

        Task<BookingDTO> GetDetailBookingById(int id);

        Task<ResponseDTO> CreateBooking(BookingAddEditDTO bookingAddEditDTO, int UserId);

        void DeleteBooking(Bookings bookings);

    }
}
