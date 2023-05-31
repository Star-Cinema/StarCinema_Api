////////////////////////////////////////////////////////////////////////////////////////////////////////
///FileName: CategoriesRepository.cs
///FileType: Visual C# Source file
///Author : VyVNK1
///Created On : 20/05/2023
///Last Modified On : 24/05/2023
///Copy Rights : FA Academy
///Description : Category Repository
////////////////////////////////////////////////////////////////////////////////////////////////////////
///
using StarCinema_Api.DTOs;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.CategoriesRepository
{
    /// <summary>
    /// VYVNK1 Create class CategoriesRepository implement interface ICategoriesRepository
    /// </summary>
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly MyDbContext _context;
        public CategoriesRepository(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// VyVNK1 METHOD: GET ALL Categories 
        /// </summary>
        /// <param name="name">for search</param>
        /// <param name="page">for pagination</param>
        /// <param name="limit">for pagination</param>
        /// <returns></returns>
        public async Task<PaginationDTO<Categories>> GetAllCategories(string? name, int page = 0, int limit = 1000)
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
                query = query.Where(s => s.Name.Contains(name));
            }
            var categories = await query.ToListAsync();
            var pagination = new PaginationDTO<Categories>();

            pagination.TotalCount = categories.Count;
            pagination.PageSize = limit;
            pagination.Page = page;
            pagination.ListItem = categories;
            return pagination;
        }


        /// <summary>
        /// VyVNK1 METHOD: GET Categories BY ID 
        /// </summary>
        /// <param name="CategoriesId">getCategoriesById</param>
        /// <returns></returns>
        public async Task<Categories> GetCategoriesById(int categoriesId)
        {

            return await _context.Categories
                .Select(x => new Categories
                {
                    Id = x.Id,
                    Name = x.Name,
                    Films = x.Films
                })
                .Where(s => s.Id == categoriesId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// VyVNK1 METHOD CREATE Categories
        /// </summary>
        /// <param name="Categories"></param>
        public void CreateCategories(Categories categories)
        {
            _context.Categories.Add(categories);
        }


        /// <summary>
        ///  VyVNK1 METHOD DELETE Categories
        /// </summary>
        /// <param name="Categories"></param>
        public void DeleteCategories(Categories categories)
        {
            _context.Categories.Remove(categories);
        }

        /// <summary>
        ///  VyVNK1  METHOD UPDATE Categories
        /// </summary>
        /// <param name="Categories"></param>
        public void UpdateCategories(Categories categories)
        {
            _context.Entry(categories).State = EntityState.Modified;
        }

        public bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
