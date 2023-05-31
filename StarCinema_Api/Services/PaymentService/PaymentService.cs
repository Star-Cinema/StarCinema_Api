using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.PaymentRepository;

namespace StarCinema_Api.Services.PaymentService
{
    /// <summary>
    /// Account : AnhNT282
    /// Description : Class service for entity payment
    /// Date created : 2023/05/19
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        /// <summary>
        /// Constructor AnhNT282
        /// </summary>
        /// <param name="paymentRepository"></param>
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        /// <summary>
        /// Create payment AnhNT282
        /// </summary>
        /// <param name="payment"></param>
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

        /// <summary>
        /// Delete payment AnhNT282
        /// </summary>
        /// <param name="id"></param>
        public async Task<ResponseDTO> DeletePayment(long id)
        {
            try
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
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        /// <summary>
        /// Get all payment AnhNT282
        /// </summary>
        public async Task<ResponseDTO> GetPaymentListAsync()
        {
            try
            {
                var listPayment = await _paymentRepository.GetPaymentListAsync();
                return new ResponseDTO()
                {
                    code = 200,
                    message = "Success",
                    data = listPayment
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }

        /// <summary>
        /// Get payment by id AnhNT282
        /// </summary>
        /// <param name="id"></param>
        public async Task<ResponseDTO> GetPaymentById(long id)
        {
            try
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
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }

        }
    }
}
