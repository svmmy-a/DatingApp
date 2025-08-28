/// <summary>
/// Data Transfer Object for user login.
/// Used by AccountController to receive login data from frontend.
/// </summary>
namespace API.DTOs;
    public class LoginDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }