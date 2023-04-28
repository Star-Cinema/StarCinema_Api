using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Images
    {
        [Key]
        public int id { get; set; }
        public int filmId { get; set; }
        public string path { get; set; }
        public virtual Films film { get; set; }
    }
}
