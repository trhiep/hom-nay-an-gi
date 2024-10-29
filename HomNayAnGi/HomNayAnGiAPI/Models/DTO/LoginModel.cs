namespace HomNayAnGiAPI.Models.DTO
{
    public class LoginModel
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
        public string? DeviceId { get; set; }
    }
}
