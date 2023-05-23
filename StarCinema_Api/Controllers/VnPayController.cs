using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.VnPayService;

namespace StarCinema_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        public VnPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost]    
        public async Task<IActionResult> CreateUrlPayment(int bookingId, double PriceTicket, double PirceService)
        {          
            var resData = await _vnPayService.CreateUrlPayment(bookingId, PriceTicket, PirceService);
            return StatusCode(resData.code, resData);
        }
        [HttpGet()]
        public async Task<IActionResult> ReturnPayment()
        {
            var vnpayData = Request.Query;
            var resData = await _vnPayService.ReturnPayment(vnpayData);
            return StatusCode(resData.code, resData);
        }

    }
}
