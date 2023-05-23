using AutoMapper;
using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;
using StarCinema_Api.Repositories.UserRepository;
using System.Security.Cryptography;
using System.Text;

namespace StarCinema_Api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public ResponseDTO ChangePassUser(ChangepassDTO changepassDTO, int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return new ResponseDTO()
            {
                code = 400,
                message = id + " dont have account",
                data = null
            };
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

            if (changepassDTO.NewPass != changepassDTO.ReNewPass) 
            return new ResponseDTO()
            {
                code = 400,
                message = "Password must be equal Re Password!",
                data = null
            };

            using var nhmac = new HMACSHA512();
            var newPass = Encoding.UTF8.GetBytes(changepassDTO.NewPass);

            user.PasswordSalt = nhmac.Key;
            user.PasswordHash = nhmac.ComputeHash(newPass);

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

        public ResponseDTO CreateUser(CreateUserDTO createUserDTO)
        {
            var user = _mapper.Map<User>(createUserDTO);

            using var hmac = new HMACSHA512();
            var passwordBytes = Encoding.UTF8.GetBytes(createUserDTO.Password);

            Random rnd = new Random();
            string verifyCode = "";
            for (int i = 0; i < 6; i++)
            {
                verifyCode += rnd.Next(0, 10).ToString();
            }
            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(passwordBytes);
            user.Token = verifyCode;
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

        public ResponseDTO DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return new ResponseDTO()
            {
                code = 400,
                message = id + " dont have user",
                data = null
            };

            user.IsDelete = true;

            //_userRepository.DeleteUser(user);
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
    }
}
