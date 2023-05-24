using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.PaymentRepository
{
    /*
        Account : AnhNT282
        Description : Interface repository for entity payment
        Date created : 2023/05/19
    */
    public interface IPaymentRepository
    {
        // Get all payment AnhNT282
        Task<List<Payment>> GetPaymentListAsync();
        // Create payment AnhNT282
        Task<bool> CreatePaymentAsync(Payment payment);
        // Delete payment AnhNT282
        void DeletePayment(Payment payment);
        // Get payment by id AnhNT282
        Task<Payment> GetPaymentById(long id);
        // Save change DbContext
        Task<bool> IsSaveChange();
        // Check the payment of the booking already exists or not AnhNT282
        Task<bool> IsPaymentOfBookingAlreadyExists(int bookingId);
    }
}
