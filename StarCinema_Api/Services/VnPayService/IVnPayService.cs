using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.VnPayService
{
    /// <summary>
    /// Account : AnhNT282
    /// Description : Interface service using vnpay for function payment
    /// Date created : 2023/05/19
    /// </summary>
    public interface IVnPayService
    {
        /// <summary>
        /// Create url payment AnhNT282
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="PriceTicket"></param>
        /// <param name="PriceService"></param>
        Task<ResponseDTO> CreateUrlPayment(int bookingID, double PriceTicket, double PriceService);
        /// <summary>
        /// Return info payment AnhNT282
        /// </summary>
        /// <param name="vnpayData"></param>
        Task<ResponseDTO> ReturnPayment(IQueryCollection vnpayData);
        /// <summary>
        /// Re payment
        /// </summary>
        /// <param name="bookingId"></param>
        Task<ResponseDTO> RePayment(int bookingId);
    }
}
