namespace HomNayAnGiApp.Models.DTO
{
    public class SignupRequestDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string SignupRequestId { get; set; }
    }
}
