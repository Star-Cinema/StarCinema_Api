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
        public int id { get; set; }
        [Required]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [MaxLength(256)]
        public string name { get; set; }
        public string? avatar { get; set; }
        [MaxLength(12)]
        [Phone]
        public string? phone { get; set; }
        public DateTime? dob { get; set; }

        [DefaultValue(false)]
        public bool? isDelete { get; set; }

        [Required]
        [Range(0, 100)]
        public int roleId { get; set; }
        [MaxLength(512)]
        public string? token { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool? gender { get; set; }
        public bool isEmailVerified { get; set; }

        public virtual Role role { get; set; }

    }
}
