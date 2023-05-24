using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.PaymentRepository;

namespace StarCinema_Api.Services.PaymentService
{
    /*
        Account : AnhNT282
        Description : Class service for entity payment
        Date created : 2023/05/19
    */
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        // Constructor AnhNT282
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        // Create payment AnhNT282
        public async Task<ResponseDTO> CreatePaymentAsync(Payment payment)
        {
            await _paymentRepository.CreatePaymentAsync(payment);
            await _paymentRepository.IsSaveChange();
            return new ResponseDTO()
            {
                code = 200,
                message = "Success"
            };
        }

        // Delete payment AnhNT282
        public async Task<ResponseDTO> DeletePayment(long id)
        {
            var payment = await _paymentRepository.GetPaymentById(id);
            if (payment == null) return new ResponseDTO()
            {
                code = 404,
                message = "Payment with this id does not exist"
            };
            _paymentRepository.DeletePayment(payment);
            await _paymentRepository.IsSaveChange();
            return new ResponseDTO()
            {
                code = 200,
                message = "Success"
            };
        }

        // Get all payment AnhNT282
        public async Task<ResponseDTO> GetPaymentListAsync()
        {
            var listPayment = await _paymentRepository.GetPaymentListAsync();
            return new ResponseDTO()
            {
                code = 200,
                message = "Success",
                data = listPayment
            };
        }

        // Get payment by id AnhNT282
        public async Task<ResponseDTO> GetPaymentById(long id)
        {
            var payment = await _paymentRepository.GetPaymentById(id);
            if (payment == null) return new ResponseDTO()
            {
                code = 404,
                message = "Payment with this id does not exist"
            };
            return new ResponseDTO()
            {
                code = 200,
                message = "Success",
                data = payment
            };
        }
    }
}
