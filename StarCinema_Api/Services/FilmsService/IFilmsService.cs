////////////////////////////////////////////////////////////////////////////////////////////////////////
///FileName: IFilmsService.cs
///FileType: Visual C# Source file
///Author : VyVNK1
///Created On : 20/05/2023
///Last Modified On : 24/05/2023
///Copy Rights : FA Academy
///Description : Category Service Interface
////////////////////////////////////////////////////////////////////////////////////////////////////////

using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.FilmsService
{
    /// <summary>
    /// VYVNK1 Create interface IFilmService
    /// </summary>
    public interface IFilmsService
    {
        Task<ResponseDTO> GetAllFilms(string? search, int page = 0, int limit = 10);
         Task<ResponseDTO> getNowShowingFilms();
        Task<ResponseDTO> getUpComingFilms();
        Task<ResponseDTO> GetFilmById(int id);
        Task<ResponseDTO> CreateFilm(FilmDTO filmDTO);
        Task<ResponseDTO> UpdateFilm(int id, FilmDTO filmDTO);
        Task<ResponseDTO> getNextShowingFilms();
        Task<ResponseDTO> DeleteFilmById(int id);
    }
}
