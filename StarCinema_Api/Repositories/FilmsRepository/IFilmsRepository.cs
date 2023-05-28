////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: IFilmsRepository.cs
//FileType: Visual C# Source file
//Author : VyVNK1
//Created On : 20/05/2023
//Last Modified On : 24/05/2023
//Copy Rights : FA Academy
//Description : Film Repository Interface
////////////////////////////////////////////////////////////////////////////////////////////////////////
///
using StarCinema_Api.DTOs;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Repositories.FilmsRepository
{
    public interface IFilmsRepository
    {
        Task<PaginationDTO<Films>> getAllFilms(string? search,
            int page = 0, int limit = 10);
        Task<List<Films>> getNowShowingFilms();
        Task<List<Films>> getUpComingFilms();
        Task<Films> getFilmById(int filmId);
        void CreateFilm(Films film);
        void CreateImage(Images image);
        void UpdateImage(int filmId, string path);
        void UpdateFilm(Films film);
        void DeleteFilm(Films film);
        bool SaveChange();
        Task<int> GetLastIDFilm();
    }
}
