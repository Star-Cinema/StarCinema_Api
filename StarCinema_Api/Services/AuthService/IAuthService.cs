using StarCinema_Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarCinema_Api.Services.AuthService
{
    
    /// <summary>
    ///    Account : HungTD34
    ///    Description : Interface of AuthService
    ///    Create : 2023/05/04
    /// </summary>

    public interface IAuthService
    {
        /// <summary>
        /// Register new account by user HungTD34
        /// </summary>
        /// <param name="registerUserDTO"></param>
        ResponseDTO Register(RegisterUserDTO registerUserDTO);
        /// <summary>
        /// Login website HungTD34
        /// </summary>
        /// <param name="authUserDTO"></param>
        ResponseDTO Login(AuthUserDTO authUserDTO);
        /// <summary>
        /// Verify email of user account HungTD34
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        ResponseDTO VerifyEmail(string email, string token);
        /// <summary>
        /// Create new password for account forgot HungTD34
        /// </summary>
        /// <param name="email"></param>
        ResponseDTO ForgotPassword(string email);
    }
}