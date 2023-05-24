using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.UserService
{
    public interface IUserService
    {
        ResponseDTO GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        ResponseDTO GetUserByEmail(string email);
        ResponseDTO GetUserById(int id);
        ResponseDTO DeleteUser(int id);
        ResponseDTO UpdateUser(UpdateUserDTO updateUserDTO, int id);
        ResponseDTO CreateUser(CreateUserDTO createUserDTO);
        ResponseDTO ChangePassUser(ChangepassDTO changepassDTO, int id);
        ResponseDTO VerifyEmail(int id);
    }
}
