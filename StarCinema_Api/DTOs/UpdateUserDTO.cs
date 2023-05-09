namespace StarCinema_Api.DTOs
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
        public DateTime? Dob { get; set; }
        public bool? Gender { get; set; }
    }
}
