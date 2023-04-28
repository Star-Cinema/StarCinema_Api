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

        public virtual Bookings booking { get; set; }
        public virtual Tickets ticket { get; set; }
        public virtual Seats seat { get; set; }
    }
}
