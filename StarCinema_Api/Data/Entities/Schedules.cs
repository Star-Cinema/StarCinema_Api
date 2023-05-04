using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Schedules
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int filmId { get; set; }

        [Required]
        public int roomId { get; set; }

        [Required]
        public DateTime startTime { get; set; }

        [Required]
        public DateTime endTime { get; set; }

        public virtual Films Film { get; set; }

        public virtual Rooms Room { get; set; }

        public virtual Tickets Ticket { get; set; }


    }
}
