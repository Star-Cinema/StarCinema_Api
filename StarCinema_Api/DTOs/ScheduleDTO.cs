using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class ScheduleDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FilmId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double Price { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        //[Required]
        //public DateTime EndTime { get; set; }

    }
}
