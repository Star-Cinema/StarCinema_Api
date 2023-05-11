using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Films
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Producer { get; set; }

        [MaxLength(255)]
        public string Director { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Duration must be greater than 0")]
        public int Duration { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Country { get; set; }

        public DateTime Release { get; set; }

        [DefaultValue(false)]
        public bool IsDelete { get; set; }

        public int CategoryId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual List<Images> Images { get; set; }
        public virtual List<Schedules> Schedules { get; set; }
    }
}
