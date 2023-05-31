using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using StarCinema_Api.Data;
using StarCinema_Api.DTOs;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.ServiceRepository
{
    /// <summary>
    /// TuNT37 service repository 
    /// </summary>
    public class ServiceRepository : BaseRepository<Data.Entities.Services>, IServiceRepository
    {
        // constructor
        public ServiceRepository(MyDbContext context) : base(context)
        {
        }

        /// <summary>
        /// TuNT37 Delete Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteService(int id)
        {
            var service = await context.Services.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (service != null)
            {
                context.Services.Remove(service);
                context.SaveChanges();
                return true;
            };
            return false;
        }

        /// <summary>
        /// TuNT37 Update Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<Data.Entities.Services> UpdateService(ServiceDTO service)
        {
            var serviceUpdate = await context.Services.Where(e => e.Id == service.Id).FirstOrDefaultAsync();
            if (serviceUpdate != null)
            {
                serviceUpdate.Name = service.Name;
                serviceUpdate.Price = service.Price;
                context.Services.Update(serviceUpdate);
                context.SaveChanges();
                return await context.Services.Where(e => e.Id == service.Id).FirstOrDefaultAsync();
            } 
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// TuNT37 Create service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<ServiceDTO> CreateService(ServiceDTO service)
        {
            var result = new Data.Entities.Services();
            result.Name = service.Name;
            result.Price = service.Price;
            context.Services.Add(result);
            context.SaveChanges();
            return service;
        }

        /// <summary>
        /// TuNT37 Get all Services
        /// </summary>
        /// <param name="keySearch"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PaginationDTO<Data.Entities.Services>> GetAllServices(string? keySearch, int page, int pageSize)
        {
            var query = (from s in context.Services
                               select new Data.Entities.Services
                               {
                                   Id = s.Id,
                                   Name = s.Name,
                                   Price = s.Price
                               }).AsQueryable();

            if(keySearch != null)
            {
                query = query.Where(e => e.Id.ToString().Contains(keySearch) || e.Name.Contains(keySearch) || e.Price.ToString().Contains(keySearch)); 
            }
            var listService = query.Distinct().ToList();
            var pagination = new PaginationDTO<Data.Entities.Services>();

            pagination.TotalCount = listService.Count;
            listService = listService.Skip(pageSize * page).Take(pageSize).ToList();
            pagination.PageSize = pageSize;
            pagination.Page = page;
            pagination.ListItem = listService;

            return pagination;
        }

        /// <summary>
        /// TuNT37 Get Service by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Data.Entities.Services> GetServiceById(int id)
        {
            return context.Services.Where(e=>e.Id == id).FirstOrDefault();
        }


    }
}
