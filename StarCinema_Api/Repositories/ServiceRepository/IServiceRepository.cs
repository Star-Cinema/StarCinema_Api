using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc;

namespace StarCinema_Api.Repositories.ServiceRepository
{
    // TuNT37 interface service repository
    public interface IServiceRepository : IBaseRepository<Data.Entities.Services>
    {
        // TuNT37 Delete Service
        Task<bool> DeleteService(int id);

        // TuNT37 Create Service
        Task<Data.Entities.Services> UpdateService(ServiceDTO service);

        // TuNT37 Create Service
        Task<ServiceDTO> CreateService(ServiceDTO service);

        // TuNT37 Get all Services by page, pageSize 
        Task<PaginationDTO<Data.Entities.Services>> GetAllServices(int page, int pageSize);

        // TuNT37 Get Service by ID
        public Task<Data.Entities.Services> GetServiceById(int id);


    }
}
