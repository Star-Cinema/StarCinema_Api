using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Rooms
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsDelete { get; set; } = false;

        public virtual List<Seats> Seats { get; set; }
        public virtual List<Schedules> Schedules { get; set; }
    }
}
