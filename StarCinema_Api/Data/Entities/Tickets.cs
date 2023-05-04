using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Tickets
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Price must be greater than or equal to 0")]
        public int Price { get; set; }

        public int ScheduleId { get; set; }

        public virtual Schedules Schedule { get; set; }
        public virtual List<BookingDetail> BookingDetails { get; set; }
    }
}
