using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.UserService;

namespace StarCinema_Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public ActionResult GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var res = _userService.GetUsers(page,pageSize,key,sortBy);
            return StatusCode(res.code, res);
        }
        //[HttpGet("{email}")]
        //public ActionResult GetUserByEmail(string email)
        //{
        //    var res = _userService.GetUserByEmail(email);
        //    return StatusCode(res.code, res);
        //}
        [HttpGet("{id}")]
        public ActionResult GetUserById(int id)
        {
            var res = _userService.GetUserById(id);
            return StatusCode(res.code, res);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var res = _userService.DeleteUser(id);
            return StatusCode(res.code, res);
        }
        [HttpPost("create")]
        public ActionResult CreateUser(CreateUserDTO createUserDTO)
        {
            var res = _userService.CreateUser(createUserDTO);
            return StatusCode(res.code, res);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateUser(UpdateUserDTO updateUserDTO, int id)
        {
            var res = _userService.UpdateUser(updateUserDTO,id);
            return StatusCode(res.code, res);
        }
        [HttpPut("changepass/{id}")]
        public ActionResult ChangePassword(ChangepassDTO changepassDTO, int id)
        {
            var res = _userService.ChangePassUser(changepassDTO, id);
            return StatusCode(res.code, res);
        }
    }
}
