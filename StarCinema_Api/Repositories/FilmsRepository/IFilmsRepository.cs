using booking_my_doctor.DTOs;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Repositories.FilmsRepository
{
    public interface IFilmsRepository
    {
        Task<PaginationDTO<Films>> getAllFilms(string? name, string? director,
            string? country, string? category,
            int page = 0, int limit = 10);
        Task<Films> getFilmById(int filmId);
        void CreateFilm(Films film);
        void CreateImage(Images image);
        void UpdateFilm(Films film);
        void DeleteFilm(Films film);
        bool SaveChange();
    }
}
