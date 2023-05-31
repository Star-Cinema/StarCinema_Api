using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.PaymentRepository
{

    /// <summary>
    /// Account : AnhNT282
    /// Description : Class repository for entity payment
    /// Date created : 2023/05/19
    /// </summary>
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MyDbContext _context;

        /// <summary>
        /// Constructor AnhNT282
        /// </summary>
        /// <param name="context"></param>
        public PaymentRepository(MyDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Create payment AnhNT282
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public async Task<bool> CreatePaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return true;
        }

        /// <summary>
        /// Delete payment AnhNT282
        /// </summary>
        /// <param name="payment"></param>
        public void DeletePayment(Payment payment)
        {
            _context.Payments.Remove(payment);
        }

        /// <summary>
        /// Get all payment AnhNT282
        /// </summary>
        /// <returns></returns>
        public async Task<List<Payment>> GetPaymentListAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        /// <summary>
        /// Check the payment of the booking already exists or not AnhNT282
        /// </summary>
        /// <param name="bookingId"></param>
        public async Task<bool> IsPaymentOfBookingAlreadyExists(int bookingId)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.bookingId== bookingId);
            if (payment == null) return false;
            return true;
        }

        /// <summary>
        /// Save change DbContext
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsSaveChange()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Get payment by id AnhNT282
        /// </summary>
        /// <param name="id"></param>
        public async Task<Payment> GetPaymentById(long id)
        {
            return await _context.Payments.FindAsync(id);
        }
    }
}
