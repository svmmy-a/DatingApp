/// <summary>
/// Data Transfer Object for user registration.
/// Used by AccountController to receive registration data from frontend.
/// </summary>
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required] // Email is required
    [EmailAddress] // Must be a valid email
    public string Email { get; set; } = "";
    [Required] // Display name is required
    public string DisplayName { get; set; } = "";
    [Required] // Password is required
    [MinLength(4)] // Password must be at least 4 chars
    public string Password { get; set; } = "";
}
 