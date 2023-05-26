using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.UserRepository
{
    /*
        Account : HungTD34
        Description : Interface of UserRepository
        Create : 2023/05/04
     */
    public interface IUserRepository
    {
        //Get list user with page, pageSize, key search, sortBy HungTD34
        List<User> GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        //Get user by email HungTD34
        User GetUserByEmail(string email);
        //Get user by id HungTD34
        User GetUserById(int id);
        //Update user HungTD34
        void UpdateUser(User user);
        //Delete user HungTD34
        void DeleteUser(User user);
        //Create new user HungTD34
        void CreateUser(User user);
        //Check database is savechange HungTD34
        bool IsSaveChange();
    }
}
