using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.UserRepository
{
    public interface IUserRepository
    {
        List<User> GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id");
        User GetUserByEmail(string email);
        User GetUserById(int id);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void CreateUser(User user);
        bool IsSaveChange();
    }
}
