using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Data.Entities
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        public int FilmId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public virtual Films Film { get; set; }
    }
}
