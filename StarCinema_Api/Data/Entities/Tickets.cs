using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Tickets
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Price must be greater than or equal to 0")]
        public int price { get; set; }

        public int scheduleId { get; set; }

        public virtual Schedules schedule { get; set; }
        public virtual List<BookingDetail> BookingDetails { get; set; }
    }
}
