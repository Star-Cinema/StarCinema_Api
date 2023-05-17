using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.FilmsService
{
    public interface IFilmsService
    {
        Task<ResponseDTO> GetAllFilms(string? name, string? director, string? country, string? category, int page = 0, int limit = 10);
        Task<ResponseDTO> GetFilmById(int id);
        Task<ResponseDTO> CreateFilm(FilmDTO filmDTO);
        Task<ResponseDTO> UpdateFilm(int id, FilmDTO filmDTO);
        Task<ResponseDTO> DeleteFilmById(int id);
    }
}
