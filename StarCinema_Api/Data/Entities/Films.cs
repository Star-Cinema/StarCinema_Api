using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Films
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(255)]
        public string name { get; set; }

        [MaxLength(255)]
        public string producer { get; set; }

        [MaxLength(255)]
        public string director { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Duration must be greater than 0")]
        public int duration { get; set; }

        [MaxLength(255)]
        public string description { get; set; }

        [MaxLength(255)]
        public string country { get; set; }

        public DateTime release { get; set; }

        [DefaultValue(false)]
        public string isDelete { get; set; }


        public virtual Categories category { get; set; }
        public virtual List<Images> images { get; set; }
        public virtual List<Schedules> Schedules { get; set; }
    }
}
