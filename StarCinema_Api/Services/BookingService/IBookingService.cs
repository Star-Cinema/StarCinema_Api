using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.BookingService
{
    public interface IBookingService
    {

        Task<ResponseDTO> GetAllBookings();

        Task<ResponseDTO> GetAllBookings(int page, int pageSize);

        Task<ResponseDTO> GetBookingById(int id);

        Task<ResponseDTO> CreateBooking(BookingDTO bookingDTO);

        Task<ResponseDTO> UpdateBooking(BookingDTO bookingDTO);
        Task<ResponseDTO> DeleteBooking(int id);

    }
}
