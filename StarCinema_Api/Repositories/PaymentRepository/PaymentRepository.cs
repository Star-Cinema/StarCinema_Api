using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.PaymentRepository
{
    /*
        Account : AnhNT282
        Description : Class repository for entity payment
        Date created : 2023/05/19
    */
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MyDbContext _context;

        // Constructor AnhNT282
        public PaymentRepository(MyDbContext context)
        {
            _context = context;
        }
        // Create payment AnhNT282
        public async Task<bool> CreatePaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return true;
        }

        // Delete payment AnhNT282
        public void DeletePayment(Payment payment)
        {
            _context.Payments.Remove(payment);
        }

        // Get all payment AnhNT282
        public async Task<List<Payment>> GetPaymentListAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        // Check the payment of the booking already exists or not AnhNT282
        public async Task<bool> IsPaymentOfBookingAlreadyExists(int bookingId)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.bookingId== bookingId);
            if (payment == null) return false;
            return true;
        }

        // Save change DbContext
        public async Task<bool> IsSaveChange()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        // Get payment by id AnhNT282
        public async Task<Payment> GetPaymentById(long id)
        {
            return await _context.Payments.FindAsync(id);
        }
    }
}
