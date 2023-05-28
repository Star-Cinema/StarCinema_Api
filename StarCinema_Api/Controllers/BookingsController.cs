using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.BookingService;
using StarCinema_Api.Services.FilmsService;

// TuNT37 Booking controller 
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

        // TuNT37 Api Get transaction history booking of user 
        [HttpGet("GetTransactionHistory")]
        public async Task<ActionResult> GetTransactionHistory(int page, int pageSize)
        {
            // Get userId by token when request create
            var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("id"))?.Value ?? "0");
            //var userId = 2;  // hard code to test
            var resData = await _bookingService.GetTransactionHistory(userId ,page, pageSize);
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Get Revenue12Month in chart of dashboard screen
        [HttpGet("GetRevenue12Month")]
        public async Task<ActionResult> GetRevenue12Month()
        {
            var resData = await _bookingService.GetRevenue12Month();
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Api get Statistical in Dashboard screen
        [HttpGet("GetStatistical")]
        public async Task<ActionResult> GetStatistical()
        {
            var resData = await _bookingService.GetStatistical();
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Api get all Seats not booked
        [HttpGet("GetSeatsNotBooked")]
        public async Task<ActionResult> GetSeatsNotBooked(int filmId, int scheduleId)
        {
            var resData = await _bookingService.GetSeatsNotBooked(filmId,scheduleId);
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Api get all Seatsof room
        [HttpGet("GetSeats")]
        public async Task<ActionResult> GetSeats(int filmId, int scheduleId)
        {
            var resData = await _bookingService.GetSeats(filmId, scheduleId);
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Admin: Api get all films to select films when booking
        [HttpGet("GetAllFilms")]
        public async Task<ActionResult> GetAllFilms()
        {
            var resData = await _bookingService.GetAllFilms();
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Api Get booking by Id
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

        // TuNT37 Api Get all booking 
        [HttpGet("")]
        public async Task<ActionResult> GetAllBookings()
        {
            var resData = await _bookingService.GetAllBookings();
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Api Get all booking by page, pagesize
        [HttpGet("GetAllByPage")]
        public async Task<ActionResult> GetAllByPage(string? keySearch, int page, int pageSize)
        {
            var resData = await _bookingService.GetAllByPage(keySearch, page, pageSize);
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Admin: Api Create booking
        [HttpPost("CreateBookingByAdmin")]
        public async Task<IActionResult> CreateBookingByAdmin(BookingAddEditDTO bookingAddEditDTO)
        {
            // Get userId by token when request create
            var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("id"))?.Value ?? "0");
            //var userId = 2;  // hard code to test

            if (bookingAddEditDTO == null)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Required request not null!!"
                });
            }
            var resData = await _bookingService.CreateBookingByAdmin(bookingAddEditDTO, userId);
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Admin: Api Create booking
        [HttpPost("CreateBookingByUser")]
        public async Task<IActionResult> CreateBookingByUser(BookingAddEditDTO bookingAddEditDTO)
        {
            // Get userId by token when request create
            var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("id"))?.Value ?? "0");
            //var userId = 2;

            if (bookingAddEditDTO == null)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Required request not null!!"
                });
            }
            var resData = await _bookingService.CreateBookingByUser(bookingAddEditDTO, userId);
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Admin: update booking
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

        // TuNT37 Admin: Api delete booking
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
