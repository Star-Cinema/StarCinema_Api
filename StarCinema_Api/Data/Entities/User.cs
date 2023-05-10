using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarCinema_Api.Data.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        [MaxLength(10)]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        public string? Avatar { get; set; }
        [MaxLength(12)]
        [Phone]
        public string? Phone { get; set; }
        public DateTime? Dob { get; set; }
        [DefaultValue(false)]
        public bool IsDelete { get; set; } = false;

        [Required]
        [Range(0, 100)]
        public int RoleId { get; set; }
        [MaxLength(512)]
        public string? Token { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? Gender { get; set; }
        public bool IsEmailVerified { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<Bookings> Bookings { get; set; }

    }
}
