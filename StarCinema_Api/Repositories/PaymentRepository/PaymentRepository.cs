using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.PaymentRepository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MyDbContext _context;

        public PaymentRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreatePaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return true;
        }

        public void DeletePayment(Payment payment)
        {
            _context.Payments.Remove(payment);
        }

        public async Task<List<Payment>> GetPaymentListAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<bool> IsPaymentOfBookingAlreadyExists(int bookingId)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.bookingId== bookingId);
            if (payment == null) return false;
            return true;
        }

        public async Task<bool> IsSaveChange()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Payment> PaymentGetPaymentById(long id)
        {
            return await _context.Payments.FindAsync(id);
        }
    }
}
