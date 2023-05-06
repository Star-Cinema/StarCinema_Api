using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class BookingService
    {
        [Key]
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int ServiceId { get; set; }

        public virtual Services Service { get; set; }
        public virtual Bookings Booking { get; set; }
    }
}
