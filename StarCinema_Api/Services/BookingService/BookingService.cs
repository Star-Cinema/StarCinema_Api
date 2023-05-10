using AutoMapper;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.BookingDetailRepository;
using StarCinema_Api.Repositories.BookingRepository;
using StarCinema_Api.Repositories.ScheduleRepository;

namespace StarCinema_Api.Services.BookingService
{
    public class BookingService : IBookingService
    {

        private readonly IBookingRepository _bookingsRepository;
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IMapper _mapper;

        public async Task<ResponseDTO> CreateBooking(BookingDTO bookingDTO)
        {
            try
            {
                var newBooking = new Bookings();
                newBooking.UserId = bookingDTO.UserId;  // get current user 
                newBooking.CreateAt = DateTime.Now;
                newBooking.Services.AddRange(bookingDTO.Services);
                await _bookingsRepository.InsertAsync(newBooking);
                _bookingsRepository.Save();

                foreach (var item in bookingDTO.BookingDetails)
                {
                    var newBookingDetail = new BookingDetail();
                    newBookingDetail.BookingId = item.BookingId;
                    newBookingDetail.TicketId = item.TicketId;
                    newBookingDetail.SeatId = item.SeatId;
                    await _bookingDetailRepository.InsertAsync(newBookingDetail);
                }
                _bookingDetailRepository.Save();

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
            _bookingsRepository.DeleteAsync(currentBooking);
            _bookingsRepository.Save();
            return new ResponseDTO
            {
                data = 200,
                message = "Success"
            };
        }

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

        public async Task<ResponseDTO> GetBookingById(int id)
        {
            try
            {
                var result = await _bookingsRepository.GetByIdAsync(id);
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
