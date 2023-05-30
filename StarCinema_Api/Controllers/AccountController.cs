using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.UserService;

namespace StarCinema_Api.Controllers
{
    ///    Account : HungTD34
    ///    Description : This class to handle user navigation to AccountService
    ///    Create : 2023/05/04

    [Route("api/my")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        //private readonly IEmailService _emailService;
        public AccountController(IUserService userService/*, IEmailService emailService*/)
        {
            _userService = userService;
            //_emailService = emailService;
        }

        //Get profile by user HungTD34
        [HttpGet]
        public ActionResult<UserDTO> My()
        {
            var userClaimsList = HttpContext.User.Claims.ToList();
            //var email = userClaimsList[3].Value.ToString();
            var res = _userService.GetUserById(Int32.Parse( userClaimsList[3].Value));
            return StatusCode(res.code, res);
        }

        //Update information by user HungTD34
        [HttpPut("update")]
        public ActionResult<UserDTO> Update(UpdateUserDTO updateUserDTO)
        {
            var userClaimsList = HttpContext.User.Claims.ToList();
            var id = (userClaimsList[3].Value);
            var res = _userService.UpdateUser(updateUserDTO, Int32.Parse(id));
            return StatusCode(res.code, res);
        }

        //Change new password by user HungTD34
        [HttpPut("changepass")]
        public ActionResult<UserDTO> ChangePassword(ChangepassDTO changepassDTO)
        {
            var userClaimsList = HttpContext.User.Claims.ToList();
            var id = (userClaimsList[3].Value);
            var res = _userService.ChangePassUser(changepassDTO, Int32.Parse(id));
            return StatusCode(res.code, res);
        }

        //Request to verify email by user HungTD34
        [HttpGet("verify")]
        public ActionResult Verify()
        {
            var claims = HttpContext.User.Claims.ToList();
            var id = claims[3].Value;
            var res = _userService.VerifyEmail(Int32.Parse(id));

            return StatusCode(res.code, res);
        }
    }
}
