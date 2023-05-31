using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.PaymentRepository
{
    /*
        
        
        
    */
    /// <summary>
    /// Account : AnhNT282
    /// Description : Interface repository for entity payment
    /// Date created : 2023/05/19
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Get all payment AnhNT282
        /// </summary>
        Task<List<Payment>> GetPaymentListAsync();
        /// <summary>
        /// Create payment AnhNT282
        /// </summary>
        /// <param name="payment"></param>
        Task<bool> CreatePaymentAsync(Payment payment);
        /// <summary>
        /// Delete payment AnhNT282
        /// </summary>
        /// <param name="payment"></param>
        void DeletePayment(Payment payment);
        /// <summary>
        /// Get payment by id AnhNT282
        /// </summary>
        /// <param name="id"></param>
        Task<Payment> GetPaymentById(long id);
        /// <summary>
        /// Save change DbContext
        /// </summary>
        Task<bool> IsSaveChange();
        /// <summary>
        /// Check the payment of the booking already exists or not AnhNT282
        /// </summary>
        /// <param name="bookingId"></param>
        Task<bool> IsPaymentOfBookingAlreadyExists(int bookingId);
    }
}
