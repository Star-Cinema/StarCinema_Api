using StarCinema_Api.Data;
using StarCinema_Api.Data.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace StarCinema_Api.Data.Seed
{
    public class Seed 
    {
        public static void SeedUsers(MyDbContext context)
        {     
            if (context.Roles.Any()) return;
            List<Role> roles = new List<Role>()
            {
                new Role() {name="admin"},
                new Role() {name="user"}
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }
    }
}