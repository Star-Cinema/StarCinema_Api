////////////////////////////////////////////////////////////////////////////////////////////////////////
///FileName: CategoriesDTO.cs
///FileType: Visual C# Source file
///Author : VyVNK1
///Created On : 20/05/2023
///Last Modified On : 24/05/2023
///Copy Rights : FA Academy
///Description : Category DTO
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.DTOs
{
    public class CategoriesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsTrash { get; set; } = false; /// for delete Category
    }
}
