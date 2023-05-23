namespace StarCinema_Api.DTOs
{
    public class CreateUserDTO
    {
        public string Email { get; set; }
        public string Name { get; set; } = "";
        public string? Avatar { get; set; } = "";
        public string? Phone { get; set; } = "0123456789";
        public DateTime? Dob { get; set; } = DateTime.Now;
        public bool? Gender { get; set; } = true;
        public int RoleId { get; set; } = 2;
        public string Password { get; set; } = "123";
    }
}
