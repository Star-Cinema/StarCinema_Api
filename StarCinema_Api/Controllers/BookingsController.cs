using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.BookingService;
using StarCinema_Api.Services.FilmsService;

namespace StarCinema_Api.Controllers
{
    /// <summary>
    /// TuNT37 Booking controller 
    /// </summary>
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

        /// <summary>
        /// TuNT37 Api Get transaction history booking of user 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("GetTransactionHistory")]
        public async Task<ActionResult> GetTransactionHistory(int page, int pageSize)
        {
            // Get userId by token when request create
            var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("id"))?.Value ?? "0");
            //var userId = 2;  // hard code to test
            var resData = await _bookingService.GetTransactionHistory(userId ,page, pageSize);
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Get Revenue12Month in chart of dashboard screen
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpGet("GetRevenue12Month")]
        public async Task<ActionResult> GetRevenue12Month()
        {
            var resData = await _bookingService.GetRevenue12Month();
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Api get Statistical in Dashboard screen
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpGet("GetStatistical")]
        public async Task<ActionResult> GetStatistical()
        {
            var resData = await _bookingService.GetStatistical();
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Api get all Seats not booked
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("GetSeatsNotBooked")]
        public async Task<ActionResult> GetSeatsNotBooked(int filmId, int scheduleId)
        {
            var resData = await _bookingService.GetSeatsNotBooked(filmId,scheduleId);
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Api get all Seatsof room
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("GetSeats")]
        public async Task<ActionResult> GetSeats(int filmId, int scheduleId)
        {
            var resData = await _bookingService.GetSeats(filmId, scheduleId);
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Admin: Api get all films to select films when booking
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAllFilms")]
        public async Task<ActionResult> GetAllFilms()
        {
            var resData = await _bookingService.GetAllFilms();
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Api Get booking by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
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

        /// <summary>
        /// TuNT37 Api Get all booking 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("")]
        public async Task<ActionResult> GetAllBookings()
        {
            var resData = await _bookingService.GetAllBookings();
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Api Get all booking by page, pagesize
        /// </summary>
        /// <param name="keySearch"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpGet("GetAllByPage")]
        public async Task<ActionResult> GetAllByPage(string? keySearch, int page, int pageSize)
        {
            var resData = await _bookingService.GetAllByPage(keySearch, page, pageSize);
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Admin: Api Create booking
        /// </summary>
        /// <param name="bookingAddEditDTO"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
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

        /// <summary>
        /// TuNT37 Admin: Api Create booking
        /// </summary>
        /// <param name="bookingAddEditDTO"></param>
        /// <returns></returns>
        [Authorize]
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

        /// <summary>
        /// TuNT37 Admin: update booking
        /// </summary>
        /// <param name="bookingDTO"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
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

        /// <summary>
        /// TuNT37 Admin: Api delete booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeteleBooking(int id)
        {
            if (id < 0)
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
