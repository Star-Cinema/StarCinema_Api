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

        public void UpdateBookingsToExpired()
        {
            var fifteenMinutesAgo = DateTime.Now.AddMinutes(-15);
            var pendingBookings = context.Bookings
                .Where(b => b.Status == "Pending" && b.CreateAt <= fifteenMinutesAgo)
                .ToList();

            foreach (var booking in pendingBookings)
            {
                booking.Status = "Expired";
            }

            context.SaveChanges();
        }

        public void UpdateBookingToSuccess(int bookingId)
        {
            var booking = context.Bookings.Where(e => e.Id == bookingId).FirstOrDefault();
            booking.Status = "Success";
            context.Bookings.Update(booking);
            context.SaveChanges();
        }

        // Get Statistical in dashboard screen
        public async Task<StatisticalDTO> GetStatistical()
        {
            var totalRevenueServicesByMonth = context.Payments
                    .Where(e=>e.CreatedDate.Month == DateTime.Today.Month).Sum(e => e.PriceService) ;
            var totalRevenueServicesByLastMonth = context.Payments
                    .Where(e => e.CreatedDate.Month == DateTime.Now.AddMonths(-1).Month ).Sum(e => e.PriceService);
            var percentRevenueGrowthServices = totalRevenueServicesByLastMonth == 0 ? 0 : ((totalRevenueServicesByMonth - totalRevenueServicesByLastMonth) / (totalRevenueServicesByLastMonth)) * 100;
            
            var totalRevenueTicketsByMonth = context.Payments
                    .Where(e => e.CreatedDate.Month == DateTime.Today.Month).Sum(e => e.PriceTicket);
            var totalRevenueTicketsByLastMonth = context.Payments
                    .Where(e => e.CreatedDate.Month == DateTime.Now.AddMonths(-1).Month ).Sum(e => e.PriceTicket);
            var percentRevenueGrowthTickets = totalRevenueTicketsByLastMonth == 0 ? 0 : ((totalRevenueTicketsByMonth - totalRevenueTicketsByLastMonth) / (totalRevenueTicketsByLastMonth)) * 100;

            var totalRevenueByMonth = totalRevenueServicesByMonth + totalRevenueTicketsByMonth;
            var totalRevenueByLastMonth = totalRevenueServicesByLastMonth + totalRevenueTicketsByLastMonth;
            var percentRevenueGrowthPrice = totalRevenueByLastMonth == 0 ? 0 : ((totalRevenueByMonth - totalRevenueByLastMonth) / (totalRevenueByLastMonth)) * 100;

            var totalRevenue = context.Payments.Sum(e => e.PriceService) + context.Payments.Sum(e => e.PriceTicket);

            return new StatisticalDTO
            {
                TotalRevenueServicesByMonth = totalRevenueServicesByMonth == null ? 0 : totalRevenueServicesByMonth,
                TotalRevenueTicketsByMonth = totalRevenueTicketsByMonth == null ? 0 : totalRevenueTicketsByMonth,
                TotalRevenueByMonth = totalRevenueByMonth == null ? 0 : totalRevenueByMonth,
                TotalRevenue = totalRevenue == null ? 0 : totalRevenue,
                PercentRevenueGrowthServices = percentRevenueGrowthServices == double.NegativeInfinity ? 0 : percentRevenueGrowthServices,
                PercentRevenueGrowthTicket = percentRevenueGrowthTickets == double.NegativeInfinity ? 0 : percentRevenueGrowthTickets,
                PercentRevenueGrowthPrice = percentRevenueGrowthPrice == double.NegativeInfinity ? 0 : percentRevenueGrowthPrice
            };
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
                         join b in context.Bookings on bd.BookingId equals b.Id
                         join t in context.Tickets on bd.TicketId equals t.Id
                         join se in context.Seats on bd.SeatId equals se.Id
                         join sc in context.Schedules on t.ScheduleId equals sc.Id
                         where sc.FilmId == filmId && sc.Id == scheduleId && !b.Status.Equals("Exprired")
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


        // Get all Seats of Room 
        public async Task<List<SeatsDTO>> GetSeats(int filmId, int scheduleId)
        {
            var listSeat = await (from s in context.Seats
                                  select new Seats
                                  {
                                      Id = s.Id,
                                      Name = s.Name
                                  }).Distinct().ToListAsync();

            var listSeatsBooked = await (from bd in context.BookingDetails
                                         join b in context.Bookings on bd.BookingId equals b.Id
                                         join t in context.Tickets on bd.TicketId equals t.Id
                                         join se in context.Seats on bd.SeatId equals se.Id
                                         join sc in context.Schedules on t.ScheduleId equals sc.Id
                                         where sc.FilmId == filmId && sc.Id == scheduleId && !b.Status.Equals("Exprired")
                                         select new Seats
                                         {
                                             Id = bd.SeatId,
                                             Name = se.Name
                                         }).Distinct().ToListAsync();

            var listSeatsNotBooked = (from l in listSeat
                                      where !(listSeatsBooked.Any(e => e.Id == l.Id))
                                      select l).ToList();

            List<SeatsDTO> listSeatsDTO = new List<SeatsDTO>();
            foreach (var item in listSeatsBooked)
            {
                var seat = new SeatsDTO();
                seat.Id = item.Id;
                seat.SeatName = item.Name;
                seat.Status = "Available";
                listSeatsDTO.Add(seat);
            }
            foreach (var item in listSeatsNotBooked)
            {
                var seat = new SeatsDTO();
                seat.Id = item.Id;
                seat.SeatName = item.Name;
                seat.Status = "UnAvailable";
                listSeatsDTO.Add(seat);
            }
            return listSeatsDTO;
        }

        // get all films to choose film when create booking
        public async Task<List<Films>> GetAllFilms()
        {
            var result = await context.Films.ToListAsync();
            return result;
        }

        public async Task<bool> CreateBookingByAdmin(BookingAddEditDTO bookingAddEditDTO, int userId)
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
            newBooking.Status = "Success";
            newBooking.Services = new List<Data.Entities.Services>();
            newBooking.Services.AddRange(listServices);
            await context.Bookings.AddAsync(newBooking);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            List<BookingDetail> listBookingDetail = new List<BookingDetail>();
            var lastBooking = await context.Bookings.OrderBy(e => e.Id).LastOrDefaultAsync();
            for (int i = 0; i < bookingAddEditDTO.ListSeatId.Count(); i++)
            {
                var newBookingDetail = new BookingDetail();
                newBookingDetail.BookingId = lastBooking.Id;
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

            try
            {
                var payment = new Payment();
                payment.bookingId = lastBooking.Id;
                payment.CreatedDate = lastBooking.CreateAt;
                payment.PriceTicket = context.BookingDetails.Include(e => e.Ticket)
                    .Where(e => e.BookingId == lastBooking.Id).Sum(x => x.Ticket.Price);
                payment.PriceService = context.Bookings.Where(e => e.Id == lastBooking.Id).FirstOrDefault().Services.Sum(e => e.Price);
                payment.ModeOfPayment = "CASH";
                await context.Payments.AddAsync(payment);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

        public async Task<BookingUserDTO> CreateBookingByUser(BookingAddEditDTO bookingAddEditDTO, int userId)
        {
            try
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
                newBooking.Status = "Pending";
                newBooking.Services = new List<Data.Entities.Services>();
                newBooking.Services.AddRange(listServices);
                await context.Bookings.AddAsync(newBooking);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            var lastBooking = await context.Bookings.OrderBy(e => e.Id).LastOrDefaultAsync();
            try
            {
                List<BookingDetail> listBookingDetail = new List<BookingDetail>();
                for (int i = 0; i < bookingAddEditDTO.ListSeatId.Count(); i++)
                {
                    var newBookingDetail = new BookingDetail();
                    newBookingDetail.BookingId = lastBooking.Id;
                    var ticket = (from t in context.Tickets
                                  join s in context.Schedules on t.ScheduleId equals s.Id
                                  where s.Id == bookingAddEditDTO.ScheduleId
                                  select new Tickets { Id = t.Id }).FirstOrDefault();
                    newBookingDetail.TicketId = ticket.Id;
                    newBookingDetail.SeatId = bookingAddEditDTO.ListSeatId[0];
                    listBookingDetail.Add(newBookingDetail);
                }
                await context.BookingDetails.AddRangeAsync(listBookingDetail);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return new BookingUserDTO
            {
                bookingId = lastBooking.Id,
                PriceTicket = context.BookingDetails.Include(e => e.Ticket)
                    .Where(e => e.BookingId == lastBooking.Id).Sum(x => x.Ticket.Price),
                PriceService = context.Bookings.Where(e => e.Id == lastBooking.Id)
                .FirstOrDefault().Services.Sum(e => e.Price)
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
