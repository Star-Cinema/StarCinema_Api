﻿////////////////////////////////////////////////////////////////////////////////////////////////////////
///FileName: ICategoriesService.cs
///FileType: Visual C# Source file
///Author : VyVNK1
///Created On : 20/05/2023
///Last Modified On : 24/05/2023
///Copy Rights : FA Academy
///Description : Category Service Interface
////////////////////////////////////////////////////////////////////////////////////////////////////////

using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.CategoriesService
{
    /// <summary>
    /// VYVNK1 Create interface ICategoriesService
    /// </summary>
    public interface ICategoriesService
    {
        Task<ResponseDTO> GetAllCategories(string? name, int page = 0, int limit = 10);
        Task<ResponseDTO> GetCategoriesById(int id);
        Task<ResponseDTO> CreateCategories(CategoriesDTO CategoriesDTO);
        Task<ResponseDTO> UpdateCategories(int id, CategoriesDTO CategoriesDTO);
        Task<ResponseDTO> DeleteCategoriesById(int id);
    }
}
