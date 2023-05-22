using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace StarCinema_Api.Repositories.BookingRepository
{
    public class BookingRepository : BaseRepository<Bookings>, IBookingRepository
    {
        public BookingRepository(MyDbContext context) : base(context)
        {
        }

        // Get all seats not booked 
        public async Task<List<Seats>> GetSeatsNotBooked(int filmId, int scheduleId)
        {
            var listSeat = await (from s in context.Seats
                                 select new Seats
                                 {
                                     Id = s.Id,
                                     Name = s.Name
                                 }).Distinct().ToListAsync();

            var listSeatsBooked = await (from bd in context.BookingDetails 
                         join t in context.Tickets on bd.TicketId equals t.Id
                         join se in context.Seats on bd.SeatId equals se.Id
                         join sc in context.Schedules on t.ScheduleId equals sc.Id
                         where sc.FilmId == filmId && sc.Id == scheduleId
                         select new Seats
                         {
                             Id = bd.SeatId,
                             Name = se.Name
                         }).Distinct().ToListAsync();

            var listSeatsNotBooked = (from l in listSeat
                                     where !(listSeatsBooked.Any(e => e.Id == l.Id))
                                     select l).ToList();

            return listSeatsNotBooked;
        }

        // get all films to choose film when create booking
        public async Task<List<Films>> GetAllFilms()
        {
            var result = await context.Films.ToListAsync();
            return result;
        }

        public async Task<ResponseDTO> CreateBooking(BookingAddEditDTO bookingAddEditDTO, int userId)
        {
            List<Data.Entities.Services> listServices = new List<Data.Entities.Services>(); 
            for (int i = 0; i < bookingAddEditDTO.ListServiceId.Count(); i++)
            {
                var service = context.Services.Where(e => e.Id == bookingAddEditDTO.ListServiceId[i]).FirstOrDefault();
                listServices.Add(service);
            }

            var newBooking = new Bookings();
            newBooking.UserId = (int)userId;  // get current user 
            newBooking.CreateAt = DateTime.Now;     // wait discus
            newBooking.Services = new List<Data.Entities.Services>();
            newBooking.Services.AddRange(listServices);
            await context.Bookings.AddAsync(newBooking);
            await context.SaveChangesAsync();

            List<BookingDetail> listBookingDetail = new List<BookingDetail>();
            for (int i = 0; i < bookingAddEditDTO.ListSeatId.Count(); i++)
            {
                var newBookingDetail = new BookingDetail();
                var _Booking = await context.Bookings.OrderBy(e=>e.Id).LastOrDefaultAsync() ;
                newBookingDetail.BookingId = _Booking.Id;
                var ticket = (from t in context.Tickets join s in context.Schedules on t.ScheduleId equals s.Id
                             where s.Id == bookingAddEditDTO.ScheduleId select new Tickets { Id = t.Id }).FirstOrDefault();
                newBookingDetail.TicketId = ticket.Id;
                newBookingDetail.SeatId = bookingAddEditDTO.ListSeatId[0];
                listBookingDetail.Add(newBookingDetail);
            }
            await context.BookingDetails.AddRangeAsync(listBookingDetail);
            try
            {
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }

            return new ResponseDTO
            {
                code = 200,
                message = "success!"
            };
        }

        public void DeleteBooking(Bookings bookings)
        {
            bookings.IsDelete = true;
            context.Bookings.Update(bookings);
            context.SaveChanges();
        }

        public async Task<PaginationDTO<BookingDTO>> GetAllBookings(int page, int pageSize)
        {
            var query2 = await (from b in context.Bookings
                                where b.IsDelete == false
                                join bd in context.BookingDetails on b.Id equals bd.BookingId
                                join u in context.Users on b.UserId equals u.Id
                                join t in context.Tickets on bd.TicketId equals t.Id
                                join s in context.Schedules on t.ScheduleId equals s.Id
                                join f in context.Films on s.FilmId equals f.Id
                                let totalPriceTickets = (context.BookingDetails.Include(e => e.Ticket)
                                            .Where(e => e.BookingId == b.Id).Sum(x => x.Ticket.Price))
                                let totalPriceServices = b.Services.Sum(e => e.Price)
                                select new BookingDTO
                                {
                                    Id = b.Id,
                                    UserId = u.Id,
                                    CreateAt = b.CreateAt,
                                    TotalPriceTickets = totalPriceTickets,
                                    TotalPriceServices = totalPriceServices,
                                    TotalPrice = totalPriceTickets + totalPriceServices,
                                    FilmName = f.Name,
                                    UserName = u.Name,
                                }).Distinct().ToListAsync();
            

            var pagination = new PaginationDTO<BookingDTO>();
            pagination.TotalCount = query2.Count;

            //query = query.Skip(pageSize * page).Take(pageSize).ToList();
            //pagination.PageSize = pageSize;
            //pagination.Page = page;
            query2 = query2.Skip(10 * 0).Take(10).ToList();
            pagination.PageSize = 10;
            pagination.Page = 0;
            pagination.ListItem = query2;

            return pagination;

        }

        public async Task<BookingDTO> GetDetailBookingById(int id)
        {
            var query = await(from b in context.Bookings
                               where b.IsDelete == false && b.Id == id
                               join bd in context.BookingDetails on b.Id equals bd.BookingId
                               join u in context.Users on b.UserId equals u.Id
                               join t in context.Tickets on bd.TicketId equals t.Id
                               join s in context.Schedules on t.ScheduleId equals s.Id
                               join f in context.Films on s.FilmId equals f.Id
                               let totalPriceTickets = (context.BookingDetails.Include(e => e.Ticket).Where(e => e.BookingId == b.Id).Sum(x => x.Ticket.Price))
                               let totalPriceServices = b.Services.Sum(e => e.Price)
                               select new BookingDTO
                               {
                                   Id = b.Id,
                                   UserId = u.Id,
                                   CreateAt = b.CreateAt,
                                   TotalPriceTickets = totalPriceTickets,
                                   TotalPriceServices = totalPriceServices,
                                   TotalPrice = totalPriceTickets + totalPriceServices,
                                   FilmName = f.Name,
                                   UserName = u.Name,
                               }).FirstOrDefaultAsync();
            return query;
        }

    }
}
