using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.ServiceRepository;
using StarCinema_Api.Services.ServiceService;

// TuNT37 Service Controller
namespace StarCinema_Api.Controllers
{
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

        // TuNT37 Api Delete Service
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

        // TuNT37 Api Update Service
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

        // TuNT37 Api Create Service
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

        // TuNT37 Api Get all Service 
        [HttpGet("GetAllServices")]
        public async Task<ActionResult> GetAllServices(int page, int pageSize)
        {
            var resData = await _serviceService.GetAllServices(page, pageSize);
            return StatusCode(resData.code, resData);
        }

        // TuNT37 Api Get Service by Id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetServiceById(int id)
        {
            if (id == null)
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
