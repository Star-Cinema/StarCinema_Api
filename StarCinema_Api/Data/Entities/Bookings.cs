using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Bookings
    {
        [Key]
        public int id { get; set; }

        [Required] 
        public int userId { get; set; }

        [Required]
        public DateTime createAt { get; set; }

        public virtual User User { get; set; }
        public virtual List<BookingDetail> BookingDetails { get; set; }
    }
}
