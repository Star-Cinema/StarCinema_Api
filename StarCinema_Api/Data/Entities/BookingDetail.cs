using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class BookingDetail
    {
        [Key]
        public int id { get; set; }
        public int bookingId { get; set; }
        public int ticketId { get; set; }
        public int seatId { get; set; }

        public virtual Bookings Booking { get; set; }
        public virtual Tickets Ticket { get; set; }
        public virtual Seats Seat { get; set; }
    }
}
