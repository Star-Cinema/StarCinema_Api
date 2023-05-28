using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.ServiceService
{
    // TuNT37 interface service
    public interface IServiceService
    {
        // Delete Service
        Task<ResponseDTO> DeleteService(int id);

        // Update Service
        Task<ResponseDTO> UpdateService(ServiceDTO services);

        // Create Service
        Task<ResponseDTO> CreateService(ServiceDTO services);

        // Get all Services
        Task<ResponseDTO> GetAllServices(string? keySearch, int page, int pageSize);

        // Get Service By Id 
        Task<ResponseDTO> GetServiceById(int id);


    }
}
