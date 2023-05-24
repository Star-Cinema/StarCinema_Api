using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StarCinema_Api.Attributes;
using StarCinema_Api.Data;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.ServiceRepository;
using StarCinema_Api.Services.ServiceService;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StarCinema_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository repository;
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceRepository repository, IServiceService serviceService)
        {
            this.repository = repository;
            _serviceService = serviceService;
        }

        // Api Get Service by Id
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

        [HttpGet(Name = "GetServices")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<ResponseDTO> GetAll(
            [FromQuery] RequestDTO input)
        {
            var result = await repository.GetAll(input.PageIndex, input.SortColumn, input.SortOrder, input.FilterQuery);
            if(result != null)
            {
                return new ResponseDTO()
                {
                    data = result,
                    message = "Success!",
                    code = 200
                };
            }
            return new ResponseDTO()
            {
                data = null,
                message = "No data in db!",
                code = 404
            };
        }

        [HttpPost]
        //[ResponseCache(NoStore = true)]
        public async Task<ResponseDTO> Post(ServiceDTO model)
        {
            if (ModelState.IsValid)
            {
                var service = await repository.Post(model);
                if (service != null)
                {
                    return new ResponseDTO()
                    {
                        data = service,
                        message = "Success!",
                        code = 200
                    };
                }
            }
            return new ResponseDTO()
            {
                data = null,
                message = "Failed!",
                code = 404
            };
        }

        [HttpDelete(Name = "DeleteService")]
        //[ResponseCache(NoStore = true)]
        public async Task<ResponseDTO> Delete(int id)
        {
            var service = await repository.Delete(id);
            if (service == true)
            {
                return new ResponseDTO()
                {
                    data = service,
                    message = "Success!",
                    code = 200
                };
            }
            return new ResponseDTO()
            {
                data = null,
                message = "Failed!",
                code = 404
            };
        }
    }
}
