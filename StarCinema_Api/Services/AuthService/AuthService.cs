using System.Security.Cryptography;
using System.Text;

using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.UserRepository;
using StarCinema_Api.Services.TokenService;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IConfiguration config)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;
        }
        public ResponseDTO Login(AuthUserDTO authUserDTO)
        {
            var currentUser = _userRepository.GetUserByEmail(authUserDTO.Email);

            if (currentUser == null)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Username is invalid!",
                    data = null
                };
            }

            using var hmac = new HMACSHA512(currentUser.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(authUserDTO.Password)
            );

            for (int i = 0; i < currentUser.PasswordHash.Length; i++)
            {
                if (currentUser.PasswordHash[i] != passwordBytes[i])
                {
                    return new ResponseDTO()
                    {
                        code = 400,
                        message = "Password is invalid!",
                        data = null
                    };
                }
            }

            return new ResponseDTO()
            {
                code = 200,
                message = "Login success",
                data = _tokenService.CreateToken(currentUser)
            };
        }
        public ResponseDTO Register(RegisterUserDTO registerUserDTO)
        {
            var currentUser = _userRepository.GetUserByEmail(registerUserDTO.Email);
            if (currentUser != null)
            {
                throw new BadHttpRequestException("Username is already existed!");
            }

            if (registerUserDTO.Password != registerUserDTO.RePassword)
            {
                throw new BadHttpRequestException("Confirm password is wrong!");
            }

            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(registerUserDTO.Password);

            Random rnd = new Random();
            string verifyCode = "";
            for (int i = 0; i < 6; i++)
            {
                verifyCode += rnd.Next(0, 10).ToString();
            }

            var newUser = new User()
            {
                Email = registerUserDTO.Email,
                Name = registerUserDTO.Name,
                IsDelete = false,
                RoleId = 2,
                Token = verifyCode,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(passwordBytes)
            };

            _userRepository.CreateUser(newUser);
            if (_userRepository.IsSaveChange()) return new ResponseDTO() { code = 200, message = "success", data = _tokenService.CreateToken(newUser) };
            else throw new BadHttpRequestException("Register faile!");
        }

        public ResponseDTO VerifyEmail(string email, string token)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(email);
                if (user != null && user.Token == token)
                {
                    user.IsEmailVerified = true;
                    _userRepository.UpdateUser(user);
                    if (_userRepository.IsSaveChange())
                        return new ResponseDTO()
                        {
                            code = 200,
                            message = "Success! Your email is verify!",
                            data = null
                        };
                    else return new ResponseDTO()
                    {
                        code = 400,
                        message = "Faile! Your email is not verify!",
                    };
                }

                else
                    return new ResponseDTO()
                    {
                        code = 400,
                        message = "Faile",
                        data = null
                    };
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Faile. " + e.Message,
                    data = null
                };
            }
        }
    }
}
