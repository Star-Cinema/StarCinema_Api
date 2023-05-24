using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Repositories.BookingRepository
{
    public interface IBookingRepository : IBaseRepository<Bookings>
    {
        // Get Transaction History of User
        Task<PaginationDTO<BookingDTO>> GetTransactionHistory(int id ,int page, int pageSize);

        void UpdateBookingsToExpired();
        void UpdateBookingToSuccess(int bookingId);

        // Get Revenue12Month in chart of dashboard screen
        Task<RevenueChartDTO> GetRevenue12Month();

        // Get Statistical in dashboard screen
        Task<StatisticalDTO> GetStatistical();

        // Admin: Get all films to choose film when create booking
        Task<List<Films>> GetAllFilms();

        // Get all Seats not booked 
        Task<List<Seats>> GetSeatsNotBooked(int filmId, int scheduleId);

        // Get all Seats of room 
        Task<List<SeatsDTO>> GetSeats(int filmId, int scheduleId);

        // Get all booking by page, pageSize 
        Task<PaginationDTO<BookingDTO>> GetAllBookings(int page, int pageSize);

        Task<BookingDTO> GetDetailBookingById(int id);

        Task<bool> CreateBookingByAdmin(BookingAddEditDTO bookingAddEditDTO, int userId);

        Task<BookingUserDTO> CreateBookingByUser(BookingAddEditDTO bookingAddEditDTO, int userId);
        void DeleteBooking(Bookings bookings);

    }
}
