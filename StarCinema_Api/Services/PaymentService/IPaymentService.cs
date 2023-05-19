using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<ResponseDTO> GetPaymentListAsync();
        Task<ResponseDTO> CreatePaymentAsync(Payment payment);
        Task<ResponseDTO> DeletePayment(long id);
        Task<ResponseDTO> PaymentGetPaymentById(long id);
    }
}
