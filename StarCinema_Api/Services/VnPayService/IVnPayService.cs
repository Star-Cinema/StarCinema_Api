using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.VnPayService
{
    public interface IVnPayService
    {
        Task<ResponseDTO> CreateUrlPayment(int bookingID);
        Task<ResponseDTO> ReturnPayment(IQueryCollection vnpayData);
    }
}
