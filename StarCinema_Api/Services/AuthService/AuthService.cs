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
using StarCinema_Api.Services.EmailService;

namespace StarCinema_Api.Services.AuthService
{
    ///    Account : HungTD34
    ///    Description : This class is for user authentication, new account registration, user email authentication
    ///    Create : 2023/05/04
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, IConfiguration config, IEmailService emailService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;
            _emailService = emailService;
        }
        //Create new password for account forgot HungTD34
        public ResponseDTO ForgotPassword(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null) return new ResponseDTO()
            {
                code = 400,
                message = "Email is nt valid"
            };

            if (!user.IsEmailVerified) return new ResponseDTO()
            {
                code = 400,
                message = "Your email is not verify"
            };

            Random rnd = new Random();
            string newPass = "";
            for (int i = 0; i < 10; i++)
            {
                newPass += rnd.Next(0, 10).ToString();
            }

            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(newPass);

            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(passwordBytes);

            //Update user with new password HungTD34
            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChange())
            {
                _emailService.SendEmail(user.Email, "New password for your account", newPass);
                return new ResponseDTO()
                {
                    code = 200,
                    message = "Success",
                    data = null
                };
            }

            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile",
                data = null
            };
        }

        //Login website HungTD34
        public ResponseDTO Login(AuthUserDTO authUserDTO)
        {
            //Check email exists HungTD34
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

            if (currentUser.IsDelete)
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Username is delete!",
                    data = null
                };

            //Decrypt the password in the database and check if it matches the entered password HungTD34
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

        //Register new account by user HungTD34
        public ResponseDTO Register(RegisterUserDTO registerUserDTO)
        {
            //Check user exists HungTD34
            var currentUser = _userRepository.GetUserByEmail(registerUserDTO.Email);
            if (currentUser != null)
            {
                throw new BadHttpRequestException("Username is already existed!");
            }

            //Check password is matches rePassword HungTD34
            if (registerUserDTO.Password != registerUserDTO.RePassword)
            {
                throw new BadHttpRequestException("Confirm password is wrong!");
            }

            //Encrypt password HungTD34
            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(registerUserDTO.Password);

            //Create random token verify HungTD34
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


            //Create new user in to database HungTD34
            _userRepository.CreateUser(newUser);
            if (_userRepository.IsSaveChange()) return new ResponseDTO() { code = 200, message = "success", data = _tokenService.CreateToken(newUser) };
            else throw new BadHttpRequestException("Register faile!");
        }

        //Verify email of user account HungTD34
        public ResponseDTO VerifyEmail(string email, string token)
        {
            try
            {
                //Check if the user exists and the token is the same HungTD34
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
