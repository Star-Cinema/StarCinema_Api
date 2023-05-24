using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.PaymentService
{
    /*
        Account : AnhNT282
        Description : Interface service for entity payment
        Date created : 2023/05/19
    */
    public interface IPaymentService
    {
        // Get all payment AnhNT282
        Task<ResponseDTO> GetPaymentListAsync();
        // Create payment AnhNT282
        Task<ResponseDTO> CreatePaymentAsync(Payment payment);
        // Delete payment AnhNT282
        Task<ResponseDTO> DeletePayment(long id);
        // Get payment by id AnhNT282
        Task<ResponseDTO> GetPaymentById(long id);
    }
}
