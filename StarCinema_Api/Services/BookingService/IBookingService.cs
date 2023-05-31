using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.BookingService
{
    /// <summary>
    /// TuNT37 interface booking service
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Get Transaction History of User
        /// </summary>
        Task<ResponseDTO> GetTransactionHistory(int id, int page, int pageSize);

        /// <summary>
        /// Get Revenue12Month in chart of dashboard screen
        /// </summary>
        Task<ResponseDTO> GetRevenue12Month();

        /// <summary>
        /// Get Statistical in dashboard screen
        /// </summary>
        Task<ResponseDTO> GetStatistical();

        /// <summary>
        /// Get all seats not booked
        /// </summary>
        Task<ResponseDTO> GetSeatsNotBooked(int filmId, int scheduleId);

        /// <summary>
        /// Get all Seats of room 
        /// </summary>
        Task<ResponseDTO> GetSeats(int filmId, int scheduleId);

        /// <summary>
        /// Admin: Get All films to choose film when create 
        /// </summary>
        Task<ResponseDTO> GetAllFilms();
        Task<ResponseDTO> GetAllBookings();

        /// <summary>
        /// Get All bookings by page, pageSize
        /// </summary>
        Task<ResponseDTO> GetAllByPage(string? keySearch, int page, int pageSize);
        /// <summary>
        /// Get Booking By Id
        /// </summary>
        Task<ResponseDTO> GetBookingById(int id);
        /// <summary>
        /// Create Booking By Admin
        /// </summary>
        Task<ResponseDTO> CreateBookingByAdmin(BookingAddEditDTO bookingAddEditDTO, int userId);
        /// <summary>
        /// Create Booking By User
        /// </summary>
        Task<ResponseDTO> CreateBookingByUser(BookingAddEditDTO bookingAddEditDTO, int userId);
        /// <summary>
        /// Update Booking
        /// </summary>
        Task<ResponseDTO> UpdateBooking(BookingDTO bookingDTO);
        /// <summary>
        /// Delete Booking
        /// </summary>
        Task<ResponseDTO> DeleteBooking(int id);

    }
}
