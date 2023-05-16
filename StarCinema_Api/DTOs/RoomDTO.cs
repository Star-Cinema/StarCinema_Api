using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StarCinema_Api.DTOs
{
    public class RoomDTO
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsDelete { get; set; }
    }
}
