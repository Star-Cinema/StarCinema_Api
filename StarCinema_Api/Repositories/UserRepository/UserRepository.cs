using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.UserRepository
{
    /// <summary>
    /// Account : HungTD34
    /// Description : This class is for manipulating the database. Handle create, update, remove, get, get list of users
    /// Create : 2023/05/04
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        public UserRepository(MyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create new user HungTD34
        /// </summary>
        /// <param name="user"></param>
        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        /// <summary>
        /// Delete user HungTD34
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        /// <summary>
        /// Get user by email HungTD34
        /// </summary>
        /// <param name="email"></param>
        public User GetUserByEmail(string email)
        {
            return _context.Users.Include(u=>u.Role).FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Update user HungTD34
        /// </summary>
        /// <param name="id"></param>
        public User GetUserById(int id)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Get list user with page, pageSize, key search, sortBy HungTD34
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="key"></param>
        /// <param name="sortBy"></param>
        public List<User> GetUsers(int? page = 1, int? pageSize = 10, string? key = "", string? sortBy = "id")
        {
            var query = _context.Users.Include(w => w.Role).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(u => u.Name.ToLower().Contains(key.ToLower()));
            }

            switch (sortBy)
            {
                case "Name":
                    query = query.OrderBy(u => u.Name);
                    break;
                default:
                    query = query.OrderBy(u => u.Id);
                    break;
            }
            if (page == null || pageSize == null || sortBy == null) { return query.ToList(); }
            else
                return query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
        }

        /// <summary>
        /// Check database is savechange HungTD34
        /// </summary>
        public bool IsSaveChange()
        {
            return _context.SaveChanges() >0;
        }

        /// <summary>
        /// Update user HungTD34
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
    }
}
