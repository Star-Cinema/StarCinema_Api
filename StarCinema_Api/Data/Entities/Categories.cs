using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Categories
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
        public virtual List<Films> Films { get; set; }
    }
}
