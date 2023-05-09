using Microsoft.EntityFrameworkCore;
using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;

namespace StarCinema_Api.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        public UserRepository(MyDbContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Include(u=>u.Role).FirstOrDefault(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
        }

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

        public bool IsSaveChange()
        {
            return _context.SaveChanges() >0;
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
    }
}
