////////////////////////////////////////////////////////////////////////////////////////////////////////
///FileName: FilmsRepository.cs
///FileType: Visual C# Source file
///Author : VyVNK1
///Created On : 20/05/2023
///Last Modified On : 24/05/2023
///Copy Rights : FA Academy
///Description : Film Repository
////////////////////////////////////////////////////////////////////////////////////////////////////////
///
using StarCinema_Api.DTOs;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.Data;



namespace StarCinema_Api.Repositories.FilmsRepository
{
    /// <summary>
    /// VYVNK1 Create class FilmsRepository implement interface IFilmsRepository
    /// </summary>
    public class FilmsRepository : IFilmsRepository
    {
        private readonly MyDbContext _context;
        public FilmsRepository(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// VyVNK1 METHOD: GET ALL FILM 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<PaginationDTO<Films>> GetAllFilms(string? search,
            int page = 0, int limit = 10)
        {
            var query = _context.Films
                 .Where(s => s.IsDelete == false)
                .Select(x => new Films
                {
                    Id = x.Id,
                    Name = x.Name,
                    Producer = x.Producer,
                    Director = x.Director,
                    Duration = x.Duration,
                    Description = x.Description,
                    Country = x.Country,
                    Release = x.Release,
                    IsDelete = x.IsDelete,
                    VideoLink = x.VideoLink,
                    Category = x.Category,
                    Images = x.Images,
                    Schedules = x.Schedules
                })
                .AsQueryable();
            if (search != null)
            {
                query = query.Where(s => s.Name.Contains(search)
                               || s.Director.Contains(search)
                               || s.Country.Contains(search)
                               || s.Producer.Contains(search)
                               || s.Category.Name.Contains(search));
            }

            var films = await query.ToListAsync();
            var pagination = new PaginationDTO<Films>();

            pagination.TotalCount = films.Count;
            pagination.PageSize = limit;
            pagination.Page = page;
            pagination.ListItem = films;
            return pagination;
        }


        /// <summary>
        /// VYVNK1 METHOD: GET ALL NOW SHOWING FILM 
        /// from list schedule, if first start date <= today <= last start date
        /// </summary>
        /// <returns></returns>

        public async Task<List<Films>> GetNowShowingFilms()
        {

            var query = _context.Films
                .Where(s => s.IsDelete == false &&
                //s.Schedules.OrderBy(e => e.StartTime).First().StartTime.Day <= DateTime.Today.Day
                s.Schedules.Any(s => DateTime.Compare(s.StartTime, DateTime.Now)  > 0 )
                //&& s.Schedules.Any(s => s.StartTime.Date >= DateTime.Today.Date)
                )
                .Select(x => new Films
                {
                    Id = x.Id,
                    Name = x.Name,
                    Producer = x.Producer,
                    Director = x.Director,
                    Duration = x.Duration,
                    Description = x.Description,
                    Country = x.Country,
                    Release = x.Release,
                    IsDelete = x.IsDelete,
                    VideoLink = x.VideoLink,
                    Category = x.Category,
                    Images = x.Images,
                    Schedules = x.Schedules
                })
                .AsQueryable();


            var films = await query.ToListAsync();

            return films;
        }

        /// <summary>
        /// VyVNK1 METHOD: GET ALL UPCOMING FILM 
        /// from list schedule, if first start date > today
        /// </summary>
        /// <returns></returns>
        public async Task<List<Films>> GetUpComingFilms()
        {
            var query = _context.Films
                 .Where(s => s.IsDelete == false &&
                          s.Schedules.OrderBy(e => e.StartTime).First().StartTime.Date > DateTime.Today.Date
                 )
                .Select(x => new Films
                {
                    Id = x.Id,
                    Name = x.Name,
                    Producer = x.Producer,
                    Director = x.Director,
                    Duration = x.Duration,
                    Description = x.Description,
                    Country = x.Country,
                    Release = x.Release,
                    IsDelete = x.IsDelete,
                    VideoLink = x.VideoLink,
                    Category = x.Category,
                    Images = x.Images,
                    Schedules = x.Schedules
                })
                .AsQueryable();


            var films = await query.ToListAsync();

            return films;
        }


        /// <summary>
        /// VyVNK1 METHOD: GET FILM BY ID 
        /// </summary>
        /// <param name="filmId"></param>
        /// <returns></returns>
        public async Task<Films> GetFilmById(int filmId)
        {
            return await _context.Films
                .Select(x => new Films
                {
                    Id = x.Id,
                    Name = x.Name,
                    Producer = x.Producer,
                    Director = x.Director,
                    Duration = x.Duration,
                    Description = x.Description,
                    Country = x.Country,
                    Release = x.Release,
                    IsDelete = x.IsDelete,
                    VideoLink = x.VideoLink,
                    Category = x.Category,
                    Images = x.Images,
                    Schedules = x.Schedules
                })
                .Where(s => s.Id == filmId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// VyVNK1 METHOD CREATE FILM
        /// </summary>
        /// <param name="film"></param>
        public void CreateFilm(Films film)
        {

            _context.Films.Add(film);
        }

        /// <summary>
        /// VyVNK1 METHOD CREATE IMAGE
        /// </summary>
        /// <param name="image"></param>
        public async void CreateImage(Images image)
        {

            _context.Images.Add(image);
            _context.SaveChanges();
        }

        /// <summary>
        /// VyVNK1 METHOD UPDATE IMAGE
        /// </summary>
        /// <param name="filmId"></param>
        /// <param name="path"></param>
        public async void UpdateImage(int filmId, string path)
        {

            var result = _context.Images.FirstOrDefault(b => b.FilmId == filmId);
            if (result != null)
            {
                result.Path = path;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// VyVNK1 METHOD DELETE FILM
        /// </summary>
        /// <param name="film"></param>
        public void DeleteFilm(Films film)
        {
            _context.Films.Remove(film);

        }

        /// <summary>
        /// VyVNK1 METHOD UPDATE FILM
        /// </summary>
        /// <param name="film"></param>
        public void UpdateFilm(Films film)
        {
            _context.Entry(film).State = EntityState.Modified;
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// VYVNK1 - METHOD GET LAST FILM ID FOR INSERT INTO IMAGES TABLE
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetLastIDFilm()
        {
            return _context.Films.OrderBy(s => s.Id).LastOrDefaultAsync().Result.Id;
        }

    }
}
