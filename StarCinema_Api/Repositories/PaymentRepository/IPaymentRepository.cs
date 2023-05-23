using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.PaymentRepository
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetPaymentListAsync();
        Task<bool> CreatePaymentAsync(Payment payment);
        void DeletePayment(Payment payment);
        Task<Payment> PaymentGetPaymentById(long id);
        Task<bool> IsSaveChange();
        Task<bool> IsPaymentOfBookingAlreadyExists(int bookingId);
    }
}
