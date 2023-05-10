using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc;

namespace StarCinema_Api.Repositories.ServiceRepository
{
    public interface IServiceRepository : IBaseRepository<StarCinema_Api.Data.Entities.Services>
    {
        public Task<Data.Entities.Services[]> GetAll(
            int pageIndex,
            string? sortColumn,
            string? sortOrder,
            string? filterQuery);

        public Task<RestDTO<StarCinema_Api.Data.Entities.Services?>> GetById(int id);
        public Task<StarCinema_Api.Data.Entities.Services?> Post(ServiceDTO model);

        public Task<bool?> Delete(int id);
    }
}
