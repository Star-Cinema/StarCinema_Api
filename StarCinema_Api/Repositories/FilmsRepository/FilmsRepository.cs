using booking_my_doctor.DTOs;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.Data;
using StarCinema_Api.Repositories.FilmsRepository;
using System.Linq.Expressions;
using StarCinema_Api.DTOs;
using System.Threading.Tasks;


namespace StarCinema_Api.Repositories.FilmsRepository
{
    public class FilmsRepository : IFilmsRepository
    {
        private readonly MyDbContext _context;
        public FilmsRepository(MyDbContext context)
        {
            _context = context;
        }

        //METHOD: GET ALL FILM 
        public async Task<PaginationDTO<Films>> getAllFilms(string? name, string? director,
            string? country, string? category,
            int page = 0, int limit = 10)
        {
            var query = _context.Films
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
                    Category = x.Category,
                    Images = x.Images,
                    Schedules = x.Schedules
                })
                .AsQueryable();
            if (name != null)
            {
                //query = query.Where(s => s.Name == name);
                query = query.Where(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
            if (director != null)
            {
                //query = query.Where(s => s.Director == director);
                query = query.Where(s => s.Director.Equals(director, StringComparison.OrdinalIgnoreCase));
            }
            if (country != null)
            {
                query = query.Where(s => s.Country.Equals(country, StringComparison.OrdinalIgnoreCase));
            }
            if (category != null)
            {
                query = query.Where(s => s.Category.Name.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            //switch (sortDate)
            //{
            //    case "asc":
            //        query = query.OrderBy(s => s.StartTime); break;
            //    case "desc":
            //        query = query.OrderByDescending(s => s.StartTime); break;
            //    default:
            //        query = query.OrderByDescending(s => s.StartTime); break;
            //}

            var films = await query.ToListAsync();
            var pagination = new PaginationDTO<Films>();

            pagination.TotalCount = films.Count;

            films = films.Skip(limit * page).Take(limit).ToList();
            pagination.PageSize = limit;
            pagination.Page = page;
            pagination.ListItem = films;
            return pagination;
        }


        // METHOD: GET FILM BY ID 
        public async Task<Films> getFilmById(int filmId)
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
                    Category = x.Category,
                    Images = x.Images,
                    Schedules = x.Schedules
                })
                .Where(s => s.Id == filmId).FirstOrDefaultAsync();
        }

        // METHOD CREATE FILM
        public void CreateFilm(Films film)
        {
            
            _context.Films.Add(film);
        }

        // METHOD CREATE IMAGE
        public void CreateImage(Images image)
        {

            _context.Images.Add(image);
        }

        // METHOD DELETE FILM
        public void DeleteFilm(Films film)
        {
            _context.Films.Remove(film);
        }

        // METHOD UPDATE FILM
        public void UpdateFilm(Films film)
        {
            _context.Entry(film).State = EntityState.Modified;
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }


    }
}
