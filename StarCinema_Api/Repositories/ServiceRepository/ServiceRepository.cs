using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using StarCinema_Api.Data;
using StarCinema_Api.DTOs;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.ServiceRepository
{
    public class ServiceRepository : BaseRepository<Data.Entities.Services>, IServiceRepository
    {
        public ServiceRepository(MyDbContext context) : base(context)
        {
        }

        // Get All service with page, pageSize 
        public async Task<PaginationDTO<Data.Entities.Services>> GetAllServices(int page, int pageSize)
        {
            return null;
        }

        // Get Service by Id
        public async Task<Data.Entities.Services> GetServiceById(int id)
        {
            var query = context.Services.Where(e=>e.Id == id).FirstOrDefault() ;
            return query;
        }

        async Task<bool?> IServiceRepository.Delete(int id)
        {
            var service = await context.Services
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            if (service != null)
            {
                context.Services.Remove(service);
                await context.SaveChangesAsync();
                return true;
            };

            return false;
        }

        void IBaseRepository<Data.Entities.Services>.DeleteAsync(Data.Entities.Services entity)
        {
            throw new NotImplementedException();
        }

        async Task<Data.Entities.Services[]> IServiceRepository.GetAll(
            int pageIndex,
            string? sortColumn,
            string? sortOrder,
            string? filterQuery)
        {
            var query = context.Services.AsQueryable();
            if (!string.IsNullOrEmpty(filterQuery))
                query = query.Where(b => b.Name.Contains(filterQuery));
            var recordCount = await query.CountAsync();
            query = query.OrderBy($"{sortColumn} {sortOrder}")
                .Skip(pageIndex * 10)
                .Take(10);
            var services = await query.ToArrayAsync();
            return services;
        }


        Task<IEnumerable<Data.Entities.Services>> IBaseRepository<Data.Entities.Services>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<RestDTO<Data.Entities.Services?>> IServiceRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<Data.Entities.Services> IBaseRepository<Data.Entities.Services>.GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Task<Data.Entities.Services> IBaseRepository<Data.Entities.Services>.InsertAsync(Data.Entities.Services entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Data.Entities.Services> Post(ServiceDTO model)
        {
            var service = await context.Services
                .Where(b => b.Id == model.Id)
                .FirstOrDefaultAsync();
            
            if (service != null)
            {
                service.Name = model.Name;
                service.Price = model.Price;
                context.Services.Update(service);
                context.SaveChanges();
            }
            else
            {
                service = new Data.Entities.Services()
                {
                    Name = model.Name!,
                    Price = model.Price,
                };
                context.Services.Add(service);
                context.SaveChanges();
            }
            return service;
        }

        void IBaseRepository<Data.Entities.Services>.Save()
        {
            throw new NotImplementedException();
        }

        void IBaseRepository<Data.Entities.Services>.Update(Data.Entities.Services entity)
        {
            throw new NotImplementedException();
        }

    }
}
