using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.BookingService
{
    // TuNT37 interface booking service
    public interface IBookingService
    {
        // Get Transaction History of User
        Task<ResponseDTO> GetTransactionHistory(int id, int page, int pageSize);

        // Get Revenue12Month in chart of dashboard screen
        Task<ResponseDTO> GetRevenue12Month();

        // Get Statistical in dashboard screen
        Task<ResponseDTO> GetStatistical();

        // Get all seats not booked
        Task<ResponseDTO> GetSeatsNotBooked(int filmId, int scheduleId);

        // Get all Seats of room 
        Task<ResponseDTO> GetSeats(int filmId, int scheduleId);

        // Admin: Get All films to choose film when create 
        Task<ResponseDTO> GetAllFilms();
        Task<ResponseDTO> GetAllBookings();

        // Get All bookings by page, pageSize
        Task<ResponseDTO> GetAllByPage(string? keySearch, int page, int pageSize);
        Task<ResponseDTO> GetBookingById(int id);
        Task<ResponseDTO> CreateBookingByAdmin(BookingAddEditDTO bookingAddEditDTO, int userId);

        Task<ResponseDTO> CreateBookingByUser(BookingAddEditDTO bookingAddEditDTO, int userId);
        Task<ResponseDTO> UpdateBooking(BookingDTO bookingDTO);
        Task<ResponseDTO> DeleteBooking(int id);

    }
}
