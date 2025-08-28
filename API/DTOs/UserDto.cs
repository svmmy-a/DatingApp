/// <summary>
/// Data Transfer Object for sending user info to frontend.
/// Includes JWT token for authentication.
/// </summary>
namespace API.DTOs;

public class UserDto
{
    public required string Id { get; set; } // User's unique ID
    public required string Email { get; set; } // User's email
    public required string DisplayName { get; set; } // User's display name
    public string? ImageURL { get; set; } // Optional profile image
    public required string Token { get; set; } // JWT token for API auth
}