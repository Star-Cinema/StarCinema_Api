using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.CategoriesService
{
    public interface ICategoriesService
    {
        Task<ResponseDTO> GetAllCategories(string? name, int page = 0, int limit = 10);
        Task<ResponseDTO> GetCategoriesById(int id);
        Task<ResponseDTO> CreateCategories(CategoriesDTO CategoriesDTO);
        Task<ResponseDTO> UpdateCategories(int id, CategoriesDTO CategoriesDTO);
        //Task<ResponseDTO> DeleteCategoriesById(int id, CategoriesDTO CategoriesDTO);
        Task<ResponseDTO> DeleteCategoriesById(int id);
    }
}
