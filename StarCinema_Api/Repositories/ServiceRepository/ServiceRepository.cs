using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using StarCinema_Api.Data;
using StarCinema_Api.DTOs;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.ServiceRepository
{
    // TuNT37 service repository 
    public class ServiceRepository : BaseRepository<Data.Entities.Services>, IServiceRepository
    {
        // constructor
        public ServiceRepository(MyDbContext context) : base(context)
        {
        }

        // TuNT37 Delete Service
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

        // TuNT37 Update Service
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

        // TuNT37 Create service
        public async Task<ServiceDTO> CreateService(ServiceDTO service)
        {
            var result = new Data.Entities.Services();
            result.Name = service.Name;
            result.Price = service.Price;
            context.Services.Add(result);
            context.SaveChanges();
            return service;
        }

        // TuNT37 Get all Services
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

        // TuNT37 Get Service by Id
        public async Task<Data.Entities.Services> GetServiceById(int id)
        {
            var query = context.Services.Where(e=>e.Id == id).FirstOrDefault() ;
            return query;
        }


    }
}
