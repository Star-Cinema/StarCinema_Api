using StarCinema_Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarCinema_Api.Services.AuthService
{
    public interface IAuthService
    {
        ResponseDTO Register(RegisterUserDTO registerUserDTO);
        ResponseDTO Login(AuthUserDTO authUserDTO);
        ResponseDTO VerifyEmail(string email, string token);
    }
}