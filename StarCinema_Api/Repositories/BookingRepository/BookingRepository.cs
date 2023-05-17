using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using System.Collections.Generic;

namespace StarCinema_Api.Repositories.BookingRepository
{
    public class BookingRepository : BaseRepository<Bookings>, IBookingRepository
    {
        public BookingRepository(MyDbContext context) : base(context)
        {

        }

        public async Task<ResponseDTO> CreateBooking(BookingDTO bookingDTO)
        {
            return new ResponseDTO
            {
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
