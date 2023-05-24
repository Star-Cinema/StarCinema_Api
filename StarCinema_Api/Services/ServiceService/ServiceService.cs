using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.ServiceRepository;

namespace StarCinema_Api.Services.ServiceService
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        // Get Service By id 
        public async Task<ResponseDTO> GetServiceById(int id)
        {
            try
            {
                var result = await _serviceRepository.GetServiceById(id);
                if (result == null)
                {
                    return new ResponseDTO
                    {
                        code = 400,
                        message = $"Does not exist booking with id {id}"
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Success",
                        data = result
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    code = 500,
                    message = ex.Message
                };
            }
        }


    }
}
