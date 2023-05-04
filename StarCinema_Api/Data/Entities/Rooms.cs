using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Rooms
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(50)]
        public string name { get; set; }

        [DefaultValue(false)]
        public string isDelete { get; set; }

        public virtual List<Seats> Seats { get; set; }
        public virtual List<Schedules> Schedules { get; set; }
    }
}
