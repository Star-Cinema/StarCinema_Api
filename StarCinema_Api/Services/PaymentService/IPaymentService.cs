using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.PaymentService
{
    /// <summary>
    /// Account : AnhNT282
    /// Description : Interface service for entity payment
    /// Date created : 2023/05/19
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Get all payment AnhNT282
        /// </summary>
        Task<ResponseDTO> GetPaymentListAsync();
        /// <summary>
        /// Create payment AnhNT282
        /// </summary>
        /// <param name="payment"></param>
        Task<ResponseDTO> CreatePaymentAsync(Payment payment);
        /// <summary>
        /// Delete payment AnhNT282
        /// </summary>
        /// <param name="id"></param>
        Task<ResponseDTO> DeletePayment(long id);
        /// <summary>
        /// Get payment by id AnhNT282
        /// </summary>
        /// <param name="id"></param>
        Task<ResponseDTO> GetPaymentById(long id);
    }
}
