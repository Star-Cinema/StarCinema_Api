using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using StarCinema_Api.Data;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Repositories.ServiceRepository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ILogger<ServiceRepository> _logger;
        private readonly MyDbContext _context;

        public ServiceRepository(ILogger<ServiceRepository> logger,
            MyDbContext context)
        {
            this._logger = logger;
            this._context = context;
        }
        
        async Task<bool?> IServiceRepository.Delete(int id)
        {
            var service = await _context.Services
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
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
            var query = _context.Services.AsQueryable();
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

        async Task<Data.Entities.Services?> IServiceRepository.Post(ServiceDTO model)
        {
            var service = await _context.Services
                .Where(b => b.Id == model.Id)
                .FirstOrDefaultAsync();
            
            if (service != null)
            {
                service.Name = model.Name;
                service.Price = model.Price;
                _context.Services.Update(service);
            }
            else
            {
                service = new Data.Entities.Services()
                {
                    Name = model.Name!,
                    Price = model.Price,
                };
                _context.Services.Add(service);
            }
            await _context.SaveChangesAsync();
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
