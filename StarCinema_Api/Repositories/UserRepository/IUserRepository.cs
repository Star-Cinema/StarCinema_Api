using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.UserRepository
{
    /// <summary>
    /// Account : HungTD34
    /// Description : Interface of UserRepository
    /// Create : 2023/05/04
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get list user with page, pageSize, key search, sortBy HungTD34
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="key"></param>
        /// <param name="sortBy"></param>
        List<User> GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        /// <summary>
        /// Get user by email HungTD34
        /// </summary>
        /// <param name="email"></param>
        User GetUserByEmail(string email);
        /// <summary>
        /// Get user by id HungTD34
        /// </summary>
        /// <param name="id"></param>
        User GetUserById(int id);
        /// <summary>
        /// Update user HungTD34
        /// </summary>
        /// <param name="user"></param>
        void UpdateUser(User user);
        /// <summary>
        /// Delete user HungTD34
        /// </summary>
        /// <param name="user"></param>
        void DeleteUser(User user);
        /// <summary>
        /// Create new user HungTD34
        /// </summary>
        /// <param name="user"></param>
        void CreateUser(User user);
        /// <summary>
        /// Check database is savechange HungTD34
        /// </summary>
        bool IsSaveChange();
    }
}
