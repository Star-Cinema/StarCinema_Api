using AutoMapper;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.BookingDetailRepository;
using StarCinema_Api.Repositories.BookingRepository;
using StarCinema_Api.Repositories.FilmsRepository;
using StarCinema_Api.Repositories.ScheduleRepository;
using StarCinema_Api.Services.PaymentService;
using StarCinema_Api.Services.VnPayService;
using System.Collections.Generic;

namespace StarCinema_Api.Services.BookingService
{
    /// <summary>
    /// TuNT37 booking service
    /// </summary>
    public class BookingService : IBookingService
    {

        private readonly IBookingRepository _bookingsRepository;
        private readonly IVnPayService _vnPayService;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingsRepository, IMapper mapper, IVnPayService vnPayService)
        {
            _bookingsRepository = bookingsRepository;
            _vnPayService = vnPayService;
            _mapper = mapper;
        }

        /// <summary>
        /// TuNT37 Get Transaction History of User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetTransactionHistory(int id, int page, int pageSize)
        {
            try
            {
                var result = await _bookingsRepository.GetTransactionHistory(id, page, pageSize);
                if (result == null)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = "Does not get all transaction history"
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Success",
                        data = result
                    };
                }
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
        /// TuNT37 Get Revenue12Month in chart of dashboard screen
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDTO> GetRevenue12Month()
        {
            try
            {
                var result = await _bookingsRepository.GetRevenue12Month();
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
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
        /// TuNT37 Get Statistical in dashboard screen
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDTO> GetStatistical()
        {
            try
            {
                var result = await _bookingsRepository.GetStatistical();
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
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
        /// TuNT37 get all seats not booked
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetSeatsNotBooked(int filmId, int scheduleId)
        {
            try
            {
                var result = await _bookingsRepository.GetSeatsNotBooked(filmId, scheduleId);
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
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
        /// TuNT37 get all seat
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetSeats(int filmId, int scheduleId)
        {
            try
            {
                var result = await _bookingsRepository.GetSeats(filmId, scheduleId);
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
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
        /// TuNT37 Admin: get all films to choose film when create booking
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDTO> GetAllFilms()
        {
            try
            {
                var result = await _bookingsRepository.GetAllFilms();
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
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
        /// TuNT37 Create booking by admin 
        /// </summary>
        /// <param name="bookingAddEditDTO"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> CreateBookingByAdmin(BookingAddEditDTO bookingAddEditDTO, int UserId)
        {
            try
            {
                _bookingsRepository.UpdateBookingsToExpired();
                var result = await _bookingsRepository.CreateBookingByAdmin(bookingAddEditDTO, UserId) ;
               
                return new ResponseDTO
                {
                    code = 200,
                    message = "success!"
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
        /// TuNT37 create booking by user 
        /// </summary>
        /// <param name="bookingAddEditDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> CreateBookingByUser(BookingAddEditDTO bookingAddEditDTO, int userId)
        {
            try
            {
                _bookingsRepository.UpdateBookingsToExpired();
                var result = await _bookingsRepository.CreateBookingByUser(bookingAddEditDTO, userId);                
                var vnPay = await _vnPayService.CreateUrlPayment(result.BookingId, result.PriceTicket, result.PriceService );
                var booking = await _bookingsRepository.GetByIdAsync(result.BookingId);
                booking.UrlPayment = (string)vnPay.data;
                _bookingsRepository.Save();
                return vnPay;
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
        /// Delete booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> DeleteBooking(int id)
        {
            var currentBooking = await _bookingsRepository.GetByIdAsync(id);
            if(currentBooking == null)
            {
                return new ResponseDTO
                {
                    code = 404,
                    message = $"Booking is not exists id {id} !"
                };
            }
            _bookingsRepository.DeleteBooking(currentBooking);
            return new ResponseDTO
            {
                data = 200,
                message = "Delete Booking Success!"
            };
        }

        /// <summary>
        /// TuNT37 get all booking
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseDTO> GetAllBookings()
        {
            try
            {
                var result = await _bookingsRepository.GetAllAsync();
                if (result == null)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = "Does not get all booking"
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Success",
                        data = result
                    };
                }
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
        /// TuNT37 Get all booking by page, pageSize 
        /// </summary>
        /// <param name="keySearch"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetAllByPage(string? keySearch, int page, int pageSize)
        {
            try
            {
                var result = await _bookingsRepository.GetAllByPage(keySearch, page, pageSize);
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
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
        /// TuNT37 Get Booking By id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetBookingById(int id)
        {
            try
            {
                var result = await _bookingsRepository.GetDetailBookingById(id);
                if (result == null) 
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"Does not exist booking with id {id}"
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Success",
                        data = result
                    };
                }
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
        /// TuNT37 update booking 
        /// </summary>
        /// <param name="bookingDTO"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> UpdateBooking(BookingDTO bookingDTO)
        {
            try
            {
                var result = await _bookingsRepository.GetByIdAsync(bookingDTO.Id);
                if(result == null)
                {
                    return new ResponseDTO
                    {
                        code = 404,
                        message = "not found!"
                    };
                }
                _bookingsRepository.Update(result);
                _bookingsRepository.Save();
                return new ResponseDTO
                {
                    code = 200,
                    message = "Update success!",
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO 
                { 
                    code = 500,
                    message = "error"
                };
            }
        }

    }
}
