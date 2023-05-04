using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Schedules
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FilmId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public virtual Films Film { get; set; }

        public virtual Rooms Room { get; set; }

        public virtual Tickets Ticket { get; set; }


    }
}
