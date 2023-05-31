using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.UserService;

namespace StarCinema_Api.Controllers
{
    
    ///    Account : HungTD34
    ///    Description : This class to handle user navigation to UserService
    ///    Create : 2023/05/04
    ///    Account : HungTD34
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Get list user with page, pageSize, key search, sortBy HungTD34
        [HttpGet]
        [Authorize(Roles ="admin")]
        public ActionResult GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var res = _userService.GetUsers(page,pageSize,key,sortBy);
            return StatusCode(res.code, res);
        }

        //Get user by id HungTD34
        [HttpGet("{id}")]
        [Authorize(Roles ="admin")]
        public ActionResult GetUserById(int id)
        {
            var res = _userService.GetUserById(id);
            return StatusCode(res.code, res);
        }

        //Disable user HungTD34
        [HttpDelete("{id}")]
        [Authorize(Roles ="admin")]
        public ActionResult DeleteUser(int id)
        {
            var res = _userService.DeleteUser(id);
            return StatusCode(res.code, res);
        }

        //Create new user HungTD34
        [HttpPost("create")]
        //[Authorize(Roles ="admin")]
        public ActionResult CreateUser(CreateUserDTO createUserDTO)
        {
            var res = _userService.CreateUser(createUserDTO);
            return StatusCode(res.code, res);
        }

        //Update user HungTD34
        [HttpPut("{id}")]
        [Authorize(Roles ="admin")]
        public ActionResult UpdateUser(UpdateUserDTO updateUserDTO, int id)
        {
            var res = _userService.UpdateUser(updateUserDTO,id);
            return StatusCode(res.code, res);
        }

        //Change new password user HungTD34
        [HttpPut("changepass/{id}")]
        [Authorize(Roles ="admin")]
        public ActionResult ChangePassword(ChangepassDTO changepassDTO, int id)
        {
            var res = _userService.ChangePassUser(changepassDTO, id);
            return StatusCode(res.code, res);
        }
    }
}
