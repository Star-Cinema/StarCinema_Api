using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class BookingDetail
    {
        [Key]
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int TicketId { get; set; }
        public int SeatId { get; set; }

        public virtual Bookings Booking { get; set; }
        public virtual Tickets Ticket { get; set; }
        public virtual Seats Seat { get; set; }
    }
}
