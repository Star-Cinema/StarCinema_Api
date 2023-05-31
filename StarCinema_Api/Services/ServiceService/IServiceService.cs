using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.ServiceService
{
    /// <summary>
    /// TuNT37 interface service
    /// </summary>
    public interface IServiceService
    {
        /// <summary>
        /// Delete Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseDTO> DeleteService(int id);

        /// <summary>
        /// Update Service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        Task<ResponseDTO> UpdateService(ServiceDTO services);

        /// <summary>
        /// Create Service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        Task<ResponseDTO> CreateService(ServiceDTO services);

        /// <summary>
        /// Get all Services
        /// </summary>
        /// <param name="keySearch"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ResponseDTO> GetAllServices(string? keySearch, int page, int pageSize);

        /// <summary>
        /// Get Service By Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseDTO> GetServiceById(int id);


    }
}
