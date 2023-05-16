using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.RoomRepository;

namespace StarCinema_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository repository;
        public RoomController(IRoomRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet(Name = "GetRooms")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<ResponseDTO> GetAll(
            [FromQuery] RequestDTO input)
        {
            var result = await repository.GetAll(input.PageIndex, input.SortColumn, input.SortOrder, input.FilterQuery);
            if (result != null)
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
        [ResponseCache(NoStore = true)]
        public async Task<ResponseDTO> AddRoom(RoomDTO model)
        {
            if (ModelState.IsValid)
            {
                var room = await repository.Add(model);
                if (room != null)
                {
                    return new ResponseDTO()
                    {
                        data = room,
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
        [HttpPut]
        [ResponseCache(NoStore = true)]
        public async Task<ResponseDTO> EditRoom(RoomDTO model)
        {
            if (ModelState.IsValid)
            {
                var room = await repository.Edit(model);
                if (room == true)
                {
                    return new ResponseDTO()
                    {
                        data = model,
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

        [HttpDelete(Name = "DeleteRoom")]
        [ResponseCache(NoStore = true)]
        public async Task<ResponseDTO> Delete(int id)
        {
            var room = await repository.Delete(id);
            if (room == true)
            {
                return new ResponseDTO()
                {
                    data = id,
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
