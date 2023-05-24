using StarCinema_Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarCinema_Api.Services.AuthService
{
    /*
        Account : HungTD34
        Description : Interface of AuthService
        Create : 2023/05/04
     */
    public interface IAuthService
    {
        //Register new account by user HungTD34
        ResponseDTO Register(RegisterUserDTO registerUserDTO);
        //Login website HungTD34
        ResponseDTO Login(AuthUserDTO authUserDTO);
        //Verify email of user account HungTD34
        ResponseDTO VerifyEmail(string email, string token);
        //Create new password for account forgot HungTD34
        ResponseDTO ForgotPassword(string email);
    }
}