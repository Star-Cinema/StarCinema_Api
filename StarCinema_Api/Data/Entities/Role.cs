using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Role
    {
        [Key]
        public int id { get; set; }
        [Required, MaxLength(50)]
        public string name { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
