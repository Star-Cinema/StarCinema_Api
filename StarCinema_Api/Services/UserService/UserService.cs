using AutoMapper;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.UserRepository;
using StarCinema_Api.Services.EmailService;
using System.Security.Cryptography;
using System.Text;

namespace StarCinema_Api.Services.UserService
{

    /// <summary>
    ///   Account : HungTD34
    ///   Description : This class is for manipulating the database. Handle create, update, disable, get, get list, verify email of users
    ///   Create : 2023/05/04
    /// </summary>

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        /// <summary>
        /// Change new password user HungTD34
        /// </summary>
        /// <param name="changepassDTO"></param>
        /// <param name="id"></param>
        public ResponseDTO ChangePassUser(ChangepassDTO changepassDTO, int id)
        {
            //Check user exists HungTD34
            var user = _userRepository.GetUserById(id);
            if (user == null) return new ResponseDTO()
            {
                code = 400,
                message = id + " dont have account",
                data = null
            };

            //Decrypt the password in the database and check if it matches the entered current password HungTD34
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(changepassDTO.CurrentPass)
            );

            for (int i = 0; i < user.PasswordHash.Length; i++)
            {
                if (user.PasswordHash[i] != passwordBytes[i])
                {
                    return new ResponseDTO()
                    {
                        code = 400,
                        message = "Password is invalid!",
                        data = null
                    };
                }
            }

            //Check if the new password entered is the same or not HungTD34
            if (changepassDTO.NewPass != changepassDTO.ReNewPass)
                return new ResponseDTO()
                {
                    code = 400,
                    message = "Password must be equal Re Password!",
                    data = null
                };

            //Encrypt new password HungTD34
            using var nhmac = new HMACSHA512();
            var newPass = Encoding.UTF8.GetBytes(changepassDTO.NewPass);

            user.PasswordSalt = nhmac.Key;
            user.PasswordHash = nhmac.ComputeHash(newPass);

            //Update user with new password HungTD34
            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChange()) return new ResponseDTO()
            {
                code = 200,
                message = "Success",
                data = null
            };
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile",
                data = null
            };
        }

        /// <summary>
        /// Create new user HungTD34
        /// </summary>
        /// <param name="createUserDTO"></param>
        public ResponseDTO CreateUser(CreateUserDTO createUserDTO)
        {
            //Mapping User from CreateUserDTO HungTD34
            var user = _mapper.Map<User>(createUserDTO);

            //Encrypt password HungTD34
            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(createUserDTO.Password);

            //Create random token verify HungTD34
            Random rnd = new Random();
            string verifyCode = "";
            for (int i = 0; i < 6; i++)
            {
                verifyCode += rnd.Next(0, 10).ToString();
            }

            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(passwordBytes);
            user.Token = verifyCode;

            //Create new user in to database HungTD34
            _userRepository.CreateUser(user);
            if (_userRepository.IsSaveChange()) return new ResponseDTO()
            {
                code = 200,
                message = "Success",
                data = null
            };
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile",
                data = null
            };
        }

        /// <summary>
        /// Disable user HungTD34
        /// </summary>
        /// <param name="id"></param>
        public ResponseDTO DeleteUser(int id)
        {
            //Check user exists HungTD34
            var user = _userRepository.GetUserById(id);
            if (user == null) return new ResponseDTO()
            {
                code = 400,
                message = id + " dont have user",
                data = null
            };

            //Disable user HungTD34
            user.IsDelete = true;


            //Update user with new IsDelete property HungTD34
            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChange()) return new ResponseDTO()
            {
                code = 200,
                message = "Success",
                data = null
            };
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile",
                data = null
            };
        }

        /// <summary>
        /// Get user by email HungTD34
        /// </summary>
        /// <param name="email"></param>
        public ResponseDTO GetUserByEmail(string email)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(email);
                if (user != null)
                    return new ResponseDTO()
                    {
                        code = 200,
                        message = "Success",
                        data = new UserDTO()
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            Phone = user.Phone,
                            Avatar = user.Avatar,
                            Dob = user.Dob,
                            Gender = user.Gender,
                            IsDelete = user.IsDelete,
                            IsEmailVerified = user.IsEmailVerified,
                            RoleDTO = new RoleDTO()
                            {
                                Id = user.Role.Id,
                                Name = user.Role.Name,
                            }
                        }
                    };
                else return new ResponseDTO()
                {
                    code = 200,
                    message = "Email is not have account",
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = ex.Message,
                    data = null
                };
            }
        }

        /// <summary>
        /// Get user by id HungTD34
        /// </summary>
        /// <param name="id"></param>
        public ResponseDTO GetUserById(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                if (user != null)
                    return new ResponseDTO()
                    {
                        code = 200,
                        message = "Success",
                        data = new UserDTO()
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            Phone = user.Phone,
                            Avatar = user.Avatar,
                            Dob = user.Dob,
                            Gender = user.Gender,
                            IsDelete = user.IsDelete,
                            IsEmailVerified = user.IsEmailVerified,
                            RoleDTO = new RoleDTO()
                            {
                                Id = user.Role.Id,
                                Name = user.Role.Name,
                            }
                        }
                    };
                else return new ResponseDTO()
                {
                    code = 200,
                    message = "Email is not have account",
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = ex.Message,
                    data = null
                };
            }
        }

        /// <summary>
        /// Get list user with page, pageSize, key search, sortBy HungTD34
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="key"></param>
        /// <param name="sortBy"></param>
        public ResponseDTO GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            try
            {
                var userDTOs = new List<UserDTO>();
                var users = _userRepository.GetUsers(page, pageSize, key, sortBy);
                foreach (var user in users)
                {
                    userDTOs.Add(new UserDTO()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Phone = user.Phone,
                        Avatar = user.Avatar,
                        Dob = user.Dob,
                        Gender = user.Gender,
                        IsDelete = user.IsDelete,
                        IsEmailVerified = user.IsEmailVerified,
                        RoleDTO = new RoleDTO()
                        {
                            Id = user.Role.Id,
                            Name = user.Role.Name,
                        }
                    });
                }
                return new ResponseDTO()
                {
                    code = 200,
                    message = "Success",
                    data = userDTOs
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO()
                {
                    code = 400,
                    message = ex.Message,
                    data = null
                };
            }
        }

        /// <summary>
        /// Update user HungTD34
        /// </summary>
        /// <param name="updateUserDTO"></param>
        /// <param name="id"></param>
        public ResponseDTO UpdateUser(UpdateUserDTO updateUserDTO, int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null) return new ResponseDTO()
            {
                code = 200,
                message = id + " is not have account",
                data = null
            };

            user.Name = updateUserDTO.Name;
            user.Avatar = updateUserDTO.Avatar;
            user.Dob = updateUserDTO.Dob;
            user.Phone = updateUserDTO.Phone;
            user.Gender = updateUserDTO.Gender;

            if (!user.IsEmailVerified && user.Email != updateUserDTO.Email)
            {
                if (_userRepository.GetUserByEmail(updateUserDTO.Email) == null)
                    user.Email = updateUserDTO.Email;
                else user.Email = "";
            }

            _userRepository.UpdateUser(user);
            if (_userRepository.IsSaveChange()) return new ResponseDTO()
            {
                code = 200,
                message = "Success",
                data = null
            };
            else return new ResponseDTO()
            {
                code = 400,
                message = "Faile",
                data = null
            };
        }

        /// <summary>
        /// Verify email of user account HungTD34
        /// </summary>
        /// <param name="id"></param>
        public ResponseDTO VerifyEmail(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null) return new ResponseDTO() { code = 400, message = "Username is not valid" };
            if (user.IsEmailVerified) return new ResponseDTO() { code = 400, message = "Your email is verify" };

            //Send email to user verify HungTD34
            return _emailService.SendEmail(user.Email, "Verify your email", "Please click this link to verify: localhost:3000/verify?email=" + user.Email.Split("@")[0] + "%40" + "gmail.com" + "&token=" + user.Token);
        }
    }
}
