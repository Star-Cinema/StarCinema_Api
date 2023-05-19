using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Services.PaymentService;

namespace StarCinema_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPaymentListAsync()
        {
            var resData = await _paymentService.GetPaymentListAsync();
            return StatusCode(resData.code, resData);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentListAsync(long id)
        {
            var resData = await _paymentService.PaymentGetPaymentById(id);
            return StatusCode(resData.code, resData);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(long id)
        {
            var resData = await _paymentService.DeletePayment(id);
            return StatusCode(resData.code, resData);
        }
    }
}
