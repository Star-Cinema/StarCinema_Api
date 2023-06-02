using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StarCinema_Api.Repositories.BookingRepository
{
    /// <summary>
    /// TuNT37 booking repository 
    /// </summary>
    public class BookingRepository : BaseRepository<Bookings>, IBookingRepository
    {
        public BookingRepository(MyDbContext context) : base(context)
        {
        }

        /// <summary>
        /// TuNT37 GEt transaction history booking of user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PaginationDTO<BookingDTO>> GetTransactionHistory(int id, int page, int pageSize)
        {
            var query = (from b in context.Bookings
                             //where b.IsDelete == false && b.Status.Equals("Success")
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
                             Status = b.Status,
                             CreateAt = b.CreateAt,
                             TotalPriceTickets = totalPriceTickets,
                             TotalPriceServices = totalPriceServices,
                             TotalPrice = totalPriceTickets + totalPriceServices,
                             FilmName = f.Name,
                             UserName = u.Name,
                         }).AsQueryable();

            if (id > 0)
            {
                query = query.Where(e => e.UserId == id);
            }

            var listBooking = query.Distinct().ToList();
            listBooking = listBooking.Skip(10 * 0).Take(10).ToList();

            var pagination = new PaginationDTO<BookingDTO>();

            pagination.TotalCount = listBooking.Count;
            pagination.PageSize = 10;
            pagination.Page = 0;
            pagination.ListItem = listBooking;

            //query = query.Skip(pageSize * page).Take(pageSize).ToList();
            //pagination.PageSize = pageSize;
            //pagination.Page = page;

            return pagination;
        }

        /// <summary>
        /// TuNT37 Set Status booking to Expired 
        /// </summary>
        public void UpdateBookingsToExpired()
        {
            var fifteenMinutesAgo = DateTime.Now.AddMinutes(-5);
            var pendingBookings = context.Bookings
                .Where(b => b.Status == "Pending" && b.CreateAt <= fifteenMinutesAgo)
                .ToList();

            foreach (var booking in pendingBookings)
            {
                booking.Status = "Expired";
            }

            context.SaveChanges();
        }

        /// <summary>
        /// TuNT37 Set Status booking to success
        /// </summary>
        /// <param name="bookingId"></param>
        public void UpdateBookingToSuccess(int bookingId)
        {
            var booking = context.Bookings.Where(e => e.Id == bookingId).FirstOrDefault();
            booking.Status = "Success";
            context.Bookings.Update(booking);
            context.SaveChanges();
        }
        public void UpdateBookingToCancel(int bookingId)
        {
            var booking = context.Bookings.Where(e => e.Id == bookingId).FirstOrDefault();
            booking.Status = "Cancel";
            context.Bookings.Update(booking);
            context.SaveChanges();
        }

        /// <summary>
        /// TuNT37 Get Statistical in dashboard screen
        /// </summary>
        /// <returns></returns>
        public async Task<StatisticalDTO> GetStatistical()
        {
            var totalRevenueServicesByMonth = context.Payments
                    .Where(e => e.CreatedDate.Month == DateTime.Today.Month).Sum(e => e.PriceService);
            var totalRevenueServicesByLastMonth = context.Payments
                    .Where(e => e.CreatedDate.Month == DateTime.Now.AddMonths(-1).Month).Sum(e => e.PriceService);
            var percentRevenueGrowthServices = totalRevenueServicesByLastMonth == 0 ? 0 : ((totalRevenueServicesByMonth - totalRevenueServicesByLastMonth) / (totalRevenueServicesByLastMonth)) * 100;

            var totalRevenueTicketsByMonth = context.Payments
                    .Where(e => e.CreatedDate.Month == DateTime.Today.Month).Sum(e => e.PriceTicket);
            var totalRevenueTicketsByLastMonth = context.Payments
                    .Where(e => e.CreatedDate.Month == DateTime.Now.AddMonths(-1).Month).Sum(e => e.PriceTicket);
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

        /// <summary>
        /// TuNT37 Get Revenue12Month in chart of dashboard screen
        /// </summary>
        /// <returns></returns>
        public async Task<RevenueChartDTO> GetRevenue12Month()
        {
            List<double> listtRevenueServices = new List<double>();
            List<double> listtRevenueTicket = new List<double>();
            for (int i = 0; i < 12; i++)
            {
                var totalRevenueServicesOfMonth = context.Payments
                    .Where(e => e.CreatedDate.Month == DateTime.Now.AddMonths(-i).Month).Sum(e => e.PriceService);
                var totalRevenueTicketsOfMonth = context.Payments
                    .Where(e => e.CreatedDate.Month == DateTime.Now.AddMonths(-i).Month).Sum(e => e.PriceTicket);
                listtRevenueServices.Add(totalRevenueServicesOfMonth);
                listtRevenueTicket.Add(totalRevenueTicketsOfMonth);
            }
            var revenueChart = new RevenueChartDTO();
            revenueChart.ListRevenueServices = listtRevenueServices;
            revenueChart.ListRevenueTickets = listtRevenueTicket;

            return revenueChart;
        }

        /// <summary>
        /// TuNT37 Get all seats not booked 
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public async Task<List<Seats>> GetSeatsNotBooked(int filmId, int scheduleId)
        {
            var roomId = context.Schedules.Where(e => e.Id == scheduleId).FirstOrDefault().RoomId;

            var listSeat = await (from s in context.Seats
                                  where s.RoomId == roomId
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
                                         join r in context.Rooms on sc.RoomId equals r.Id
                                         where sc.FilmId == filmId && sc.Id == scheduleId
                                            //&& !b.Status.Equals("Exprired") 
                                            && b.Status.Equals("Success")
                                            && r.Id == roomId
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


        /// <summary>
        /// TuNT37 Get all Seats of Room 
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public async Task<List<SeatsDTO>> GetSeats(int filmId, int scheduleId)
        {
            var roomId = context.Schedules.Where(e => e.Id == scheduleId).FirstOrDefault().RoomId;

            var listSeat = await (from s in context.Seats
                                  where s.RoomId == roomId
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
                                         join r in context.Rooms on sc.RoomId equals r.Id
                                         where sc.FilmId == filmId && sc.Id == scheduleId
                                         //&& !b.Status.Equals("Exprired") 
                                         && b.Status.Equals("Success")
                                         && r.Id == roomId
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
                seat.Status = "Unavailable";
                listSeatsDTO.Add(seat);
            }
            foreach (var item in listSeatsNotBooked)
            {
                var seat = new SeatsDTO();
                seat.Id = item.Id;
                seat.SeatName = item.Name;
                seat.Status = "Available";
                listSeatsDTO.Add(seat);
            }
            return listSeatsDTO;
        }

        /// <summary>
        /// TuNT37 get all films to choose film when create booking
        /// </summary>
        /// <returns></returns>
        public async Task<List<Films>> GetAllFilms()
        {
            var result = await context.Films.Where(e => e.IsDelete == false).ToListAsync();
            return result;
        }

        /// <summary>
        /// TuNT37 create booking by admin 
        /// </summary>
        /// <param name="bookingAddEditDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> CreateBookingByAdmin(BookingAddEditDTO bookingAddEditDTO, int userId)
        {
            try
            {
                List<Data.Entities.Services> listServices = new List<Data.Entities.Services>();
                if (bookingAddEditDTO.ListServiceId != null)
                {
                    for (int i = 0; i < bookingAddEditDTO.ListServiceId.Count(); i++)
                    {
                        var service = context.Services.Where(e => e.Id == bookingAddEditDTO.ListServiceId[i]).FirstOrDefault();
                        listServices.Add(service);
                    }
                }

                var newBooking = new Bookings();
                newBooking.UserId = (int)userId;  // get current user 
                newBooking.CreateAt = DateTime.Now;     // wait discus
                newBooking.Status = "Success";
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
                    newBookingDetail.SeatId = bookingAddEditDTO.ListSeatId[i];
                    listBookingDetail.Add(newBookingDetail);
                }
                await context.BookingDetails.AddRangeAsync(listBookingDetail);
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

        /// <summary>
        /// TuNT37 create booking by user 
        /// </summary>
        /// <param name="bookingAddEditDTO"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<BookingUserDTO> CreateBookingByUser(BookingAddEditDTO bookingAddEditDTO, int userId)
        {
            try
            {
                List<Data.Entities.Services> listServices = new List<Data.Entities.Services>();
                if (bookingAddEditDTO.ListServiceId != null)
                {
                    for (int i = 0; i < bookingAddEditDTO.ListServiceId.Count(); i++)
                    {
                        var service = context.Services.Where(e => e.Id == bookingAddEditDTO.ListServiceId[i]).FirstOrDefault();
                        listServices.Add(service);
                    }
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
                    newBookingDetail.SeatId = bookingAddEditDTO.ListSeatId[i];
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
                BookingId = lastBooking.Id,
                PriceTicket = context.BookingDetails.Include(e => e.Ticket)
                    .Where(e => e.BookingId == lastBooking.Id).Sum(x => x.Ticket.Price),
                PriceService = context.Bookings.Where(e => e.Id == lastBooking.Id)
                .FirstOrDefault().Services.Sum(e => e.Price)
            };
        }

        /// <summary>
        /// TuNT37 delete booking 
        /// </summary>
        /// <param name="bookings"></param>
        public void DeleteBooking(Bookings bookings)
        {
            bookings.IsDelete = true;
            context.Bookings.Update(bookings);
            context.SaveChanges();
        }

        /// <summary>
        /// TuNT37 GEt all booking with page / pageSize
        /// </summary>
        /// <param name="keySearch"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PaginationDTO<BookingDTO>> GetAllByPage(string? keySearch, int page, int pageSize)
        {
            //var query = (from b in context.Bookings
            //             join p in context.Payments on b.Id equals p.bookingId
            //             join bd in context.BookingDetails on b.Id equals bd.BookingId
            //             join u in context.Users on b.UserId equals u.Id
            //             join t in context.Tickets on bd.TicketId equals t.Id
            //             join s in context.Schedules on t.ScheduleId equals s.Id
            //             join f in context.Films on s.FilmId equals f.Id
            //             where b.IsDelete == false
            //             select new BookingDTO
            //             {
            //                 Id = b.Id,
            //                 UserId = u.Id,
            //                 CreateAt = b.CreateAt,
            //                 Status = b.Status,
            //                 TotalPriceTickets = p.PriceTicket == null ? 0 : p.PriceTicket,
            //                 TotalPriceServices = p.PriceService == null ? 0 : p.PriceService,
            //                 TotalPrice = p.PriceTicket + p.PriceService,
            //                 FilmName = f.Name,
            //                 UserName = u.Name,
            //             }).AsQueryable();

            var query = (from b in context.Bookings
                         join bd in context.BookingDetails on b.Id equals bd.BookingId
                         join u in context.Users on b.UserId equals u.Id
                         join t in context.Tickets on bd.TicketId equals t.Id
                         join s in context.Schedules on t.ScheduleId equals s.Id
                         join f in context.Films on s.FilmId equals f.Id
                         let totalPriceTickets = (context.BookingDetails.Include(e => e.Ticket)
                                            .Where(e => e.BookingId == b.Id).Sum(x => x.Ticket.Price))
                         let totalPriceServices = b.Services.Sum(e => e.Price)

                         where b.IsDelete == false
                         select new BookingDTO
                         {
                             Id = b.Id,
                             UserId = u.Id,
                             CreateAt = b.CreateAt,
                             Status = b.Status,
                             TotalPriceTickets = totalPriceTickets,
                             TotalPriceServices = totalPriceServices,
                             TotalPrice = totalPriceTickets + totalPriceServices,
                             FilmName = f.Name,
                             UserName = u.Name,
                         }).AsQueryable();

            //var query2 = (from b in context.Bookings
            //              join p in context.Payments on b.Id equals p.bookingId
            //              join bd in context.BookingDetails on b.Id equals bd.BookingId
            //              join u in context.Users on b.UserId equals u.Id
            //              join t in context.Tickets on bd.TicketId equals t.Id
            //              join s in context.Schedules on t.ScheduleId equals s.Id
            //              join f in context.Films on s.FilmId equals f.Id
            //              where b.IsDelete == false
            //              select new BookingDTO
            //              {
            //                  Id = b.Id,
            //                  UserId = u.Id,
            //                  CreateAt = b.CreateAt,
            //                  Status = b.Status,
            //                  FilmName = f.Name,
            //                  UserName = u.Name,
            //              }).AsQueryable();

            //if (keySearch != null)
            //{
            //    query2 = query2.Where(e => e.FilmName.Contains(keySearch) || e.UserName.Contains(keySearch)
            //                || e.Id.Equals(keySearch));
            //}

            //var listBooking2 = query2.Distinct().ToList();


            if (keySearch != null)
            {
                query = query.Where(e => e.FilmName.Contains(keySearch) || e.UserName.Contains(keySearch)
                            || e.Id.Equals(keySearch));
            }

            var listBooking = query.Distinct().ToList();
            var pagination = new PaginationDTO<BookingDTO>();

            pagination.TotalCount = listBooking.Count;
            listBooking = listBooking.Skip(pageSize * page).Take(pageSize).ToList();
            pagination.PageSize = pageSize;
            pagination.Page = page;
            pagination.ListItem = listBooking;

            return pagination;
        }

        /// <summary>
        /// TuNT37 get detail booking by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BookingDTO> GetDetailBookingById(int id)
        {
            var query = await (from b in context.Bookings
                               join p in context.Payments on b.Id equals p.bookingId
                               join bd in context.BookingDetails on b.Id equals bd.BookingId
                               join u in context.Users on b.UserId equals u.Id
                               join t in context.Tickets on bd.TicketId equals t.Id
                               join s in context.Schedules on t.ScheduleId equals s.Id
                               join f in context.Films on s.FilmId equals f.Id
                               where b.IsDelete == false
                               select new BookingDTO
                               {
                                   Id = b.Id,
                                   UserId = u.Id,
                                   CreateAt = b.CreateAt,
                                   Status = b.Status,
                                   TotalPriceTickets = p.PriceTicket == null ? 0 : p.PriceTicket,
                                   TotalPriceServices = p.PriceService == null ? 0 : p.PriceService,
                                   TotalPrice = p.PriceTicket + p.PriceService,
                                   FilmName = f.Name,
                                   UserName = u.Name,
                               }).FirstOrDefaultAsync();
            return query;
        }

        public void AddUrlPayment(Bookings booking, string urlPayment)
        {
            context.Entry(booking).State = EntityState.Modified;
        }
    }
}
