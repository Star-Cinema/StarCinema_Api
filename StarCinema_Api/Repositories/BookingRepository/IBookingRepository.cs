using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Repositories.BookingRepository
{
    /// <summary>
    /// TuNT37 interface booking repository 
    /// </summary>
    public interface IBookingRepository : IBaseRepository<Bookings>
    {
        /// <summary>
        /// Get Transaction History of User
        /// </summary>
        Task<PaginationDTO<BookingDTO>> GetTransactionHistory(int id ,int page, int pageSize);

        /// <summary>
        /// change status booking to expired 
        /// </summary>
        void UpdateBookingsToExpired();

        /// <summary>
        /// change status booking to success 
        /// </summary>
        void UpdateBookingToSuccess(int bookingId);
        /// <summary>
        /// change status booking to cancel 
        /// </summary>
        void UpdateBookingToCancel(int bookingId);

        /// <summary>
        /// Get Revenue12Month in chart of dashboard screen
        /// </summary>
        Task<RevenueChartDTO> GetRevenue12Month();

        /// <summary>
        /// Get Statistical in dashboard screen
        /// </summary>
        Task<StatisticalDTO> GetStatistical();

        /// <summary>
        /// Admin: Get all films to choose film when create booking
        /// </summary>
        Task<List<Films>> GetAllFilms();

        /// <summary>
        /// Get all Seats not booked 
        /// </summary>
        Task<List<Seats>> GetSeatsNotBooked(int filmId, int scheduleId);

        /// <summary>
        /// Get all Seats of room 
        /// </summary>
        Task<List<SeatsDTO>> GetSeats(int filmId, int scheduleId);

        /// <summary>
        /// Get all booking by page, pageSize 
        /// </summary>
        Task<PaginationDTO<BookingDTO>> GetAllByPage(string? keySearch, int page, int pageSize);

        /// <summary>
        /// Get Detail Booking By Id
        /// </summary>
        Task<BookingDTO> GetDetailBookingById(int id);

        /// <summary>
        /// Create Booking By Admin
        /// </summary>
        Task<bool> CreateBookingByAdmin(BookingAddEditDTO bookingAddEditDTO, int userId);

        /// <summary>
        /// Create Booking By User
        /// </summary>
        Task<BookingUserDTO> CreateBookingByUser(BookingAddEditDTO bookingAddEditDTO, int userId);
        /// <summary>
        /// Delete Booking
        /// </summary>
        void DeleteBooking(Bookings bookings);
        /// <summary>
        /// Add field urlPayment
        /// </summary>
        void AddUrlPayment(Bookings booking, string urlPayment);

    }
}
