using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.BookingService;
using StarCinema_Api.Services.FilmsService;

namespace StarCinema_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        // construction and injection
        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // Api get all Seats not booked
        [HttpGet("GetSeatsNotBooked")]
        public async Task<ActionResult> GetSeatsNotBooked(int filmId, int scheduleId)
        {
            var resData = await _bookingService.GetSeatsNotBooked(filmId,scheduleId);
            return StatusCode(resData.code, resData);
        }

        // Admin: Api get all films to select films when booking
        [HttpGet("GetAllFilms")]
        public async Task<ActionResult> GetAllFilms()
        {
            var resData = await _bookingService.GetAllFilms();
            return StatusCode(resData.code, resData);
        }

        // Api Get booking by Id
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

        // Api Get all booking 
        [HttpGet("")]
        public async Task<ActionResult> GetAllBookings()
        {
            var resData = await _bookingService.GetAllBookings();
            return StatusCode(resData.code, resData);
        }

        // Api Get all booking by page, pagesize
        [HttpGet("GetAllByPage")]
        public async Task<ActionResult> GetAllByPage(int page, int pageSize)
        {
            var resData = await _bookingService.GetAllBookings(page, pageSize);
            return StatusCode(resData.code, resData);
        }

        // Admin: Api Create booking
        [HttpPost]
        public async Task<IActionResult> Createbooking(BookingAddEditDTO bookingAddEditDTO)
        {
            // Get userId by token when request create
            var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("id"))?.Value ?? "0");
            //var userId = 3;

            if (bookingAddEditDTO == null)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Required request not null!!"
                });
            }
            var resData = await _bookingService.CreateBooking(bookingAddEditDTO, userId);
            return StatusCode(resData.code, resData);
        }

        // Admin: update booking
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

        // Admin: Api delete booking
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
