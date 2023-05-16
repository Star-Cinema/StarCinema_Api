using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.BookingService;

namespace StarCinema_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookingById(int id)
        {
            if(id == null)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Required request not null!!"
                });
            }
            var resData = await _bookingService.GetBookingById(id);
            return StatusCode(resData.code, resData);
        }

        [HttpGet("")]
        public async Task<ActionResult> GetAllBookings()
        {
            var resData = await _bookingService.GetAllBookings();
            return StatusCode(resData.code, resData);
        }

        [HttpPost]
        public async Task<IActionResult> Createbooking(BookingDTO bookingDTO)
        {
            if (bookingDTO == null)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Required request not null!!"
                });
            }
            var resData = await _bookingService.CreateBooking(bookingDTO);
            return StatusCode(resData.code, resData);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking(BookingDTO bookingDTO)
        {
            if (bookingDTO == null)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Required request not null!!"
                });
            }
            var resData = await _bookingService.UpdateBooking(bookingDTO);
            return StatusCode(resData.code, resData);
        }

        [HttpDelete]
        public async Task<IActionResult> DeteleBooking(int id)
        {
            if (id == null)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Required request not null!!"
                });
            }
            var resData = await _bookingService.DeleteBooking(id);
            return StatusCode(resData.code, resData);
        }


    }
}
