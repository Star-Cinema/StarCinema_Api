using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsTrash { get; set; } = false; // for delete Category
        public virtual List<Films> Films { get; set; }
    }
}
