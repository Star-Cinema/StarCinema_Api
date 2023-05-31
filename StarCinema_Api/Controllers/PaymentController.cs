using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Services.PaymentService;

namespace StarCinema_Api.Controllers
{
    /// <summary>
    /// Account : AnhNT282
    /// Description : Class controller for entity Payment
    /// Date created : 2023/05/19
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    

    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        /// <summary>
        /// Constructor AnhNT282
        /// </summary>
        /// <param name="paymentService"></param>
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Get all payment AnhNT282
        /// </summary>
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetPaymentListAsync()
        {
            var resData = await _paymentService.GetPaymentListAsync();
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// Get payment by id AnhNT282
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymenByIdAsync(long id)
        {
            var resData = await _paymentService.GetPaymentById(id);
            return StatusCode(resData.code, resData);
        }
        /// <summary>
        /// Delete payment AnhNT282
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(long id)
        {
            var resData = await _paymentService.DeletePayment(id);
            return StatusCode(resData.code, resData);
        }
    }
}
