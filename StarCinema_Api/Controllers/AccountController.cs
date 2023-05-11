using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.UserService;

namespace StarCinema_Api.Controllers
{
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
        [HttpGet]
        public ActionResult<UserDTO> My()
        {
            var userClaimsList = HttpContext.User.Claims.ToList();
            var email = userClaimsList[2].Value.ToString();
            var res = _userService.GetUserByEmail(userClaimsList[2].Value.ToString());
            return StatusCode(res.code, res);
        }
        [HttpPut("update")]
        public ActionResult<UserDTO> Update(UpdateUserDTO updateUserDTO)
        {
            var userClaimsList = HttpContext.User.Claims.ToList();
            var id = (userClaimsList[3].Value);
            var res = _userService.UpdateUser(updateUserDTO, Int32.Parse(id));
            return StatusCode(res.code, res);
        }
        [HttpPut("changepass")]
        public ActionResult<UserDTO> ChangePassword(ChangepassDTO changepassDTO)
        {
            var userClaimsList = HttpContext.User.Claims.ToList();
            var id = (userClaimsList[3].Value);
            var res = _userService.ChangePassUser(changepassDTO, Int32.Parse(id));
            return StatusCode(res.code, res);
        }
        //[HttpGet("verify")]
        //public ActionResult Verify()
        //{
        //    var claims = HttpContext.User.Claims.ToList();
        //    var username = claims[0].Value.ToString();
        //    var user = _userService.GetUserByUsername(username);

        //    try
        //    {
        //        var body = "Please click this link to verify: https://localhost:7125/api/verify?email=" + user.email.Split("@")[0] + "%40" + "gmail.com" + "&token=" + user.phone;
        //        var res = _emailService.SendEmail(user.email, "Verify your account", body);
        //        res.message = "Please check your email to verify this account!";
        //        return StatusCode(res.code, res);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Message);
        //    }
        //    return Ok();
        //}
    }
}
