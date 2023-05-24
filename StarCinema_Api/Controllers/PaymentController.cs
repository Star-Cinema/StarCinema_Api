using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Services.PaymentService;

namespace StarCinema_Api.Controllers
{
    /*
        Account : AnhNT282
        Description : Class controller for entity Payment
        Date created : 2023/05/19
     */

    [Route("api/[controller]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        // Constructor AnhNT282
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // Get all payment AnhNT282
        [HttpGet]
        public async Task<IActionResult> GetPaymentListAsync()
        {
            var resData = await _paymentService.GetPaymentListAsync();
            return StatusCode(resData.code, resData);
        }

        // Get payment by id AnhNT282
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymenByIdAsync(long id)
        {
            var resData = await _paymentService.GetPaymentById(id);
            return StatusCode(resData.code, resData);
        }
        // Delete payment AnhNT282
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(long id)
        {
            var resData = await _paymentService.DeletePayment(id);
            return StatusCode(resData.code, resData);
        }
    }
}
