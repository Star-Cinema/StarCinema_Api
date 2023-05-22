using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.BookingService
{
    public interface IBookingService
    {
        // Get all seats not booked
        Task<ResponseDTO> GetSeatsNotBooked(int filmId, int scheduleId);

        // Admin: Get All films to choose film when create 
        Task<ResponseDTO> GetAllFilms();
        Task<ResponseDTO> GetAllBookings();

        // Get All bookings by page, pageSize
        Task<ResponseDTO> GetAllBookings(int page, int pageSize);
        Task<ResponseDTO> GetBookingById(int id);
        Task<ResponseDTO> CreateBooking(BookingAddEditDTO bookingAddEditDTO, int userId);
        Task<ResponseDTO> UpdateBooking(BookingDTO bookingDTO);
        Task<ResponseDTO> DeleteBooking(int id);

    }
}
