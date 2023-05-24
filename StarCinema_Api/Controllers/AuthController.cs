using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarCinema_Api.DTOs;
using StarCinema_Api.Services.AuthService;
using StarCinema_Api.Services.UserService;

namespace StarCinema_Api.Controllers
{
    /*
        Account : HungTD34
        Description : This class to handle user navigation to AuthService
        Create : 2023/05/04
     */
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        //Login website HungTD34
        [HttpPost("login")]
        public ActionResult Login(AuthUserDTO authUserDTO)
        {
            var res = _authService.Login(authUserDTO);
            return StatusCode(res.code, res);
        }

        //Register new account by user HungTD34
        [HttpPost("register")]
        public ActionResult Register(RegisterUserDTO registerUserDTO)
        {
            var res = _authService.Register(registerUserDTO);
            return StatusCode(res.code, res);
        }

        //Verify email of user account HungTD34
        [HttpPost("verify")]
        public ActionResult VerifyEmail(string email, string token)
        {
            try
            {
                var res = _authService.VerifyEmail(email, token);
                return StatusCode(res.code, res);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        //Forgot password by user HungTD34
        [HttpGet("forgot")]
        public ActionResult ForgotPassword(string email)
        {
            var res = _authService.ForgotPassword(email);
            return StatusCode(res.code, res);
        }
    }
}
