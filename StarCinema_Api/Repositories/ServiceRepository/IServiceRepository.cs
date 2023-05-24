using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc;

namespace StarCinema_Api.Repositories.ServiceRepository
{
    public interface IServiceRepository : IBaseRepository<Data.Entities.Services>
    {

        // Get all Service with page, pageSize 
        Task<PaginationDTO<Data.Entities.Services>> GetAllServices(int page, int pageSize);

        // Get Service by ID
        public Task<Data.Entities.Services> GetServiceById(int id);


        public Task<Data.Entities.Services[]> GetAll(
            int pageIndex,
            string? sortColumn,
            string? sortOrder,
            string? filterQuery);

        public Task<RestDTO<Data.Entities.Services?>> GetById(int id);
        public Task<Data.Entities.Services> Post(ServiceDTO model);

        public Task<bool?> Delete(int id);
    }
}
