////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: ICategoriesRepository.cs
//FileType: Visual C# Source file
//Author : VyVNK1
//Created On : 20/05/2023
//Last Modified On : 24/05/2023
//Copy Rights : FA Academy
//Description : Category Repository Interface
////////////////////////////////////////////////////////////////////////////////////////////////////////

using StarCinema_Api.DTOs;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.CategoriesRepository
{
    public interface ICategoriesRepository
    {
        Task<PaginationDTO<Categories>> getAllCategories(string? name, int page = 0, int limit = 10);
        Task<Categories> getCategoriesById(int CategoriesId);
        void CreateCategories(Categories Categories);
        void UpdateCategories(Categories Categories);
        void DeleteCategories(Categories Categories);
        bool SaveChange();
    }
}
