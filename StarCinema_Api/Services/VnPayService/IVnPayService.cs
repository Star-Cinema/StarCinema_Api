using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.VnPayService
{
    /*
        Account : AnhNT282
        Description : Interface service using vnpay for function payment
        Date created : 2023/05/19
    */
    public interface IVnPayService
    {
        // Create url payment AnhNT282
        Task<ResponseDTO> CreateUrlPayment(int bookingID, double PriceTicket, double PriceService);
        // Return info payment AnhNT282
        Task<ResponseDTO> ReturnPayment(IQueryCollection vnpayData);
    }
}
