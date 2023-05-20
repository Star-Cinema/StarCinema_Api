using AutoMapper;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.BookingDetailRepository;
using StarCinema_Api.Repositories.BookingRepository;
using StarCinema_Api.Repositories.FilmsRepository;
using StarCinema_Api.Repositories.ScheduleRepository;
using System.Collections.Generic;

namespace StarCinema_Api.Services.BookingService
{
    public class BookingService : IBookingService
    {

        private readonly IBookingRepository _bookingsRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingsRepository, IMapper mapper)
        {
            _bookingsRepository = bookingsRepository;
            _mapper = mapper;
        }

        // get all seats not booked
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

        // Admin: get all films to choose film when create booking
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

        // Create booking 
        public async Task<ResponseDTO> CreateBooking(BookingAddEditDTO bookingAddEditDTO, int UserId)
        {
            try
            {
                var result = await _bookingsRepository.CreateBooking(bookingAddEditDTO, UserId) ;
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

        // Delete booking
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

        // Get all booking by page, pageSize 
        public async Task<ResponseDTO> GetAllBookings(int page, int pageSize)
        {
            try
            {
                var result = await _bookingsRepository.GetAllBookings(page, pageSize);
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

        // Get Booking By id 
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
