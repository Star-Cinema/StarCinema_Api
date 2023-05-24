////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: FilmDTO.cs
//FileType: Visual C# Source file
//Author : VyVNK1
//Created On : 20/05/2023
//Last Modified On : 24/05/2023
//Copy Rights : FA Academy
//Description : Film DTO
////////////////////////////////////////////////////////////////////////////////////////////////////////
///
using StarCinema_Api.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace StarCinema_Api.DTOs
{
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
