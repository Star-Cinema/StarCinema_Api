using StarCinema_Api.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace StarCinema_Api.DTOs
{
    //*************************
    // vy comment
    //return the field of film you would like to display
    //To accomplish this, we'll modify the controller methods to return a data transfer object (DTO) instead of the EF model
    //*************************
    public class FilmDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public DateTime Release { get; set; }

        [DefaultValue(false)]
        public bool IsDelete { get; set; } = false;
        public string VideoLink { get; set; }
        public int CategoryId { get; set; }
        public List<ImageDTO> Image { get; set; }

    }
}
