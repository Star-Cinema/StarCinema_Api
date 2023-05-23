using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.DTOs
{
    public class CategoriesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsTrash { get; set; } = false; // for delete Category
    }
}
