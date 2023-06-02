using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.VnPayService;

namespace StarCinema_Api.Controllers
{

    /// <summary>
    /// Account : AnhNT282
    /// Description : Class controller using vnpay for function payment
    /// Date created : 2023/05/18
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        /// <summary>
        /// Constructor AnhNT282
        /// </summary>
        /// <param name="vnPayService"></param>
        public VnPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        /// <summary>
        /// Create url payment AnhNT282
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="PriceTicket"></param>
        /// <param name="PirceService"></param>
        [Authorize]
        [HttpPost]    
        public async Task<IActionResult> CreateUrlPayment(int bookingId, double PriceTicket, double PirceService)
        {          
            var resData = await _vnPayService.CreateUrlPayment(bookingId, PriceTicket, PirceService);
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// Return info payment
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ReturnPayment()
        {
            var vnpayData = Request.Query;
            var resData = await _vnPayService.ReturnPayment(vnpayData);
            return StatusCode(resData.code, resData);
        }
        /// <summary>
        /// Re payment
        /// </summary>
        [HttpGet(Name ="re-payment")]
        public async Task<IActionResult> RePayment(int bookingId)
        {
            var vnpayData = Request.Query;
            var resData = await _vnPayService.RePayment(vnpayData);
            return StatusCode(resData.code, resData);
        }

    }
}
