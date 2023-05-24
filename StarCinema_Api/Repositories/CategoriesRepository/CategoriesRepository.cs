////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: CategoriesRepository.cs
//FileType: Visual C# Source file
//Author : VyVNK1
//Created On : 20/05/2023
//Last Modified On : 24/05/2023
//Copy Rights : FA Academy
//Description : Category Repository
////////////////////////////////////////////////////////////////////////////////////////////////////////
///
using StarCinema_Api.DTOs;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.CategoriesRepository
{
    // VYVNK1 Create class CategoriesRepository implement interface ICategoriesRepository
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly MyDbContext _context;
        public CategoriesRepository(MyDbContext context)
        {
            _context = context;
        }

        //VyVNK1 METHOD: GET ALL Categories 
        public async Task<PaginationDTO<Categories>> getAllCategories(string? name, int page = 0, int limit = 1000)
        {
            var query = _context.Categories
                .Where(s => s.IsTrash == false)
                .Select(x => new Categories
                {
                    Id = x.Id,
                    Name = x.Name,
                    Films = x.Films
                })
                .AsQueryable();
            if (name != null)
            {
                
                query = query.Where(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
            var Categories = await query.ToListAsync();
            var pagination = new PaginationDTO<Categories>();

            pagination.TotalCount = Categories.Count;
            //Categories = Categories.Skip(limit * page).Take(limit).ToList();
            pagination.PageSize = limit;
            pagination.Page = page;
            pagination.ListItem = Categories;
            return pagination;
        }


        // VyVNK1 METHOD: GET Categories BY ID 
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

        // VyVNK1 METHOD CREATE Categories
        public void CreateCategories(Categories Categories)
        {
            _context.Categories.Add(Categories);
        }


        //  VyVNK1 METHOD DELETE Categories
        public void DeleteCategories(Categories Categories)
        {
            _context.Categories.Remove(Categories);
        }

        //  VyVNK1  METHOD UPDATE Categories
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
