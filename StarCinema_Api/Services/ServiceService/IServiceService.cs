using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.ServiceService
{
    public interface IServiceService
    {
        Task<ResponseDTO> GetServiceById(int id);


    }
}
