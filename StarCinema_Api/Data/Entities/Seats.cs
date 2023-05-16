using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Seats
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public int RoomId { get; set; }

        [DefaultValue(false)]
        public bool IsDelete { get; set; } = false;

        public virtual Rooms Room { get; set; }
        public virtual List<BookingDetail> BookingDetails { get; set; }
    }
}
