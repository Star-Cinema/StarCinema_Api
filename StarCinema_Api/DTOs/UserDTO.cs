using StarCinema_Api.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StarCinema_Api.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
        public DateTime? Dob { get; set; }
        public bool? IsDelete { get; set; }
        public bool? Gender { get; set; }
        public bool IsEmailVerified { get; set; }
        public virtual RoleDTO RoleDTO { get; set; }
    }
}
