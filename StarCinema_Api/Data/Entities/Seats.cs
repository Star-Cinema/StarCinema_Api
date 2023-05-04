using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Seats
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(50)]
        public string name { get; set; }

        [Required]
        [MaxLength(50)]
        public int roomId { get; set; }

        [DefaultValue(false)]
        public string isDelete { get; set; }

        public virtual Rooms Room { get; set; }
        public virtual List<BookingDetail> BookingDetails { get; set; }
    }
}
