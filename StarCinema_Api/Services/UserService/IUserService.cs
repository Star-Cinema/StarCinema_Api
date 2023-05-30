using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.UserService
{
    
    ///    Account : HungTD34
    ///    Description : Interface of UserService
    ///    Create : 2023/05/04
     
    public interface IUserService
    {
        //Get list user with page, pageSize, key search, sortBy HungTD34
        ResponseDTO GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        //Get user by email HungTD34
        ResponseDTO GetUserByEmail(string email);
        //Get user by id HungTD34
        ResponseDTO GetUserById(int id);
        //Disable user HungTD34
        ResponseDTO DeleteUser(int id);
        //Update user HungTD34
        ResponseDTO UpdateUser(UpdateUserDTO updateUserDTO, int id);
        //Create new user HungTD34
        ResponseDTO CreateUser(CreateUserDTO createUserDTO);
        //Change new password user HungTD34
        ResponseDTO ChangePassUser(ChangepassDTO changepassDTO, int id);
        //Verify email of user account HungTD34
        ResponseDTO VerifyEmail(int id);
    }
}
