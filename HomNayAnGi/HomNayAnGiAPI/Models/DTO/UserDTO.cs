namespace HomNayAnGiAPI.Models.DTO
{
    public class UserDTO
    {
        public string? UserId { get; set; }
        public string? Username { get; set; } = null!;
        public string?  Email { get; set; }
        public string? Password { get; set; } = null!;
        public string?  CreatedAt { get; set; }
        public string? Role { get; set; } = null!;
        public string? IsActive { get; set; }
    }
}
