using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc;

namespace StarCinema_Api.Repositories.ServiceRepository
{
    /// <summary>
    /// TuNT37 interface service repository
    /// </summary>
    public interface IServiceRepository : IBaseRepository<Data.Entities.Services>
    {
        /// <summary>
        /// TuNT37 Delete Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteService(int id);

        /// <summary>
        /// TuNT37 Create Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        Task<Data.Entities.Services> UpdateService(ServiceDTO service);

        /// <summary>
        /// TuNT37 Create Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        Task<ServiceDTO> CreateService(ServiceDTO service);

        /// <summary>
        /// TuNT37 Get all Services by page, pageSize 
        /// </summary>
        /// <param name="keySearch"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PaginationDTO<Data.Entities.Services>> GetAllServices(string? keySearch, int page, int pageSize);

        /// <summary>
        /// TuNT37 Get Service by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Data.Entities.Services> GetServiceById(int id);


    }
}
