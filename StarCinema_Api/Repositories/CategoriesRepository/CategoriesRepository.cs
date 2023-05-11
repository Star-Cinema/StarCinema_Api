using booking_my_doctor.DTOs;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.CategoriesRepository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly MyDbContext _context;
        public CategoriesRepository(MyDbContext context)
        {
            _context = context;
        }

        //METHOD: GET ALL Categories 
        public async Task<PaginationDTO<Categories>> getAllCategories(string? name, int page = 0, int limit = 10)
        {
            var query = _context.Categories
                .Select(x => new Categories
                {
                    Id = x.Id,
                    Name = x.Name,
                    Films = x.Films
                })
                .AsQueryable();
            if (name != null)
            {
                //query = query.Where(s => s.Name == name);
                query = query.Where(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
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

            var Categories = await query.ToListAsync();
            var pagination = new PaginationDTO<Categories>();

            pagination.TotalCount = Categories.Count;

            Categories = Categories.Skip(limit * page).Take(limit).ToList();
            pagination.PageSize = limit;
            pagination.Page = page;
            pagination.ListItem = Categories;
            return pagination;
        }


        // METHOD: GET Categories BY ID 
        public async Task<Categories> getCategoriesById(int CategoriesId)
        {

            return await _context.Categories

                .Select(x => new Categories
                {
                    Id = x.Id,
                    Name = x.Name,
                    Films = x.Films
                })
                .Where(s => s.Id == CategoriesId).FirstOrDefaultAsync();
        }

        // METHOD CREATE Categories
        public void CreateCategories(Categories Categories)
        {

            _context.Categories.Add(Categories);
        }

       
        // METHOD DELETE Categories
        public void DeleteCategories(Categories Categories)
        {
            _context.Categories.Remove(Categories);
        }

        // METHOD UPDATE Categories
        public void UpdateCategories(Categories Categories)
        {
            _context.Entry(Categories).State = EntityState.Modified;
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
