using StarCinema_Api.Data.Entities;
using StarCinema_Api.DTOs;

namespace StarCinema_Api.Services.UserService
{
    
    /// <summary>
    ///    Account : HungTD34
    ///    Description : Interface of UserService
    ///    Create : 2023/05/04
    /// </summary>
     
    public interface IUserService
    {
        /// <summary>
        /// Get list user with page, pageSize, key search, sortBy HungTD34
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="key"></param>
        /// <param name="sortBy"></param>
        ResponseDTO GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        /// <summary>
        /// Get user by email HungTD34
        /// </summary>
        /// <param name="email"></param>
        ResponseDTO GetUserByEmail(string email);
        /// <summary>
        /// Get user by id HungTD34
        /// </summary>
        /// <param name="id"></param>
        ResponseDTO GetUserById(int id);
        /// <summary>
        /// Disable user HungTD34
        /// </summary>
        /// <param name="id"></param>
        ResponseDTO DeleteUser(int id);
        /// <summary>
        /// Update user HungTD34
        /// </summary>
        /// <param name="updateUserDTO"></param>
        /// <param name="id"></param>
        ResponseDTO UpdateUser(UpdateUserDTO updateUserDTO, int id);
        /// <summary>
        /// Create new user HungTD34
        /// </summary>
        /// <param name="createUserDTO"></param>
        ResponseDTO CreateUser(CreateUserDTO createUserDTO);
        /// <summary>
        /// Change new password user HungTD34
        /// </summary>
        /// <param name="changepassDTO"></param>
        /// <param name="id"></param>
        ResponseDTO ChangePassUser(ChangepassDTO changepassDTO, int id);
        /// <summary>
        /// Verify email of user account HungTD34
        /// </summary>
        /// <param name="id"></param>
        ResponseDTO VerifyEmail(int id);
    }
}
