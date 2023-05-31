using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.ServiceRepository;
using StarCinema_Api.Services.ServiceService;

namespace StarCinema_Api.Controllers
{
    /// <summary>
    /// TuNT37 Service Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        // TuNT37 constructor
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        /// <summary>
        /// TuNT37 Api Delete Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteService")]
        public async Task<IActionResult> DeleteService(int id)
        {
            if (id == null)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Request Required not null!!"
                });
            }
            var resData = await _serviceService.DeleteService(id);
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Api Update Service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPut("UpdateService")]
        public async Task<IActionResult> UpdateService(ServiceDTO services)
        {
            if (services == null)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Request Required not null!!"
                });
            }
            var resData = await _serviceService.UpdateService(services);
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Api Create Service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService(ServiceDTO services)
        {
            if (services == null)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Request Required not null!!"
                });
            }
            var resData = await _serviceService.CreateService(services);
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Api Get all Service 
        /// </summary>
        /// <param name="keySearch"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetAllServices")]
        public async Task<ActionResult> GetAllServices(string? keySearch, int page, int pageSize)
        {
            var resData = await _serviceService.GetAllServices(keySearch, page, pageSize);
            return StatusCode(resData.code, resData);
        }

        /// <summary>
        /// TuNT37 Api Get Service by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetServiceById(int id)
        {
            if (id < 0)
            {
                return BadRequest(new ResponseDTO
                {
                    message = "Required request not null!!"
                });
            }
            var resData = await _serviceService.GetServiceById(id);
            return StatusCode(resData.code, resData);
        }


    }
}
