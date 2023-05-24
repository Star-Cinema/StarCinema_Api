////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: Images.cs
//FileType: Visual C# Source file
//Author : VyVNK1
//Created On : 20/05/2023
//Last Modified On : 24/05/2023
//Copy Rights : FA Academy
//Description : Images Entity
////////////////////////////////////////////////////////////////////////////////////////////////////////

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
