using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.DTOs
{
    public class ServiceDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double Price { get; set; }
    }
}
