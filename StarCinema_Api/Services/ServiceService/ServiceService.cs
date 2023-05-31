using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.ServiceRepository;
using System.ComponentModel.DataAnnotations;

namespace StarCinema_Api.Services.ServiceService
{
    /// <summary>
    /// TuNT37 Service service
    /// </summary>
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        /// <summary>
        /// TuNT37 constructor
        /// </summary>
        /// <param name="serviceRepository"></param>
        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        /// <summary>
        /// TuNT37 Delete Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> DeleteService(int id)
        {
            try
            {
                var result = await _serviceRepository.DeleteService(id);
                if (result == true)
                {
                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Delete Success",
                        data = result
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Delete UnSucccess!",
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

        /// <summary>
        /// TuNT37 Update Service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> UpdateService(ServiceDTO services)
        {
            try
            {
                var result = await _serviceRepository.UpdateService(services);
                if(result != null)
                {
                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Update Success",
                        data = result
                    };
                } else
                {
                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Update UnSucccess!",
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

        /// <summary>
        /// TuNT37 Create Service 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> CreateService(ServiceDTO services)
        {
            try
            {
                var result = await _serviceRepository.CreateService(services);
                if(result != null)
                {
                    return new ResponseDTO
                    {
                        code = 200,
                        message = "Create Success",
                        data = result
                    };
                } else
                {
                    return new ResponseDTO
                    {
                        code = 500,
                        message = "Create UnSuccess!"
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

        /// <summary>
        /// TuNT37 Get All Services with page, pageSize
        /// </summary>
        /// <param name="keySearch"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetAllServices(string? keySearch, int page, int pageSize)
        {
            try
            {
                var result = await _serviceRepository.GetAllServices(keySearch, page, pageSize);
                return new ResponseDTO
                {
                    code = 200,
                    message = "Success",
                    data = result
                };
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

        /// <summary>
        /// TuNT37 Get Service By id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
