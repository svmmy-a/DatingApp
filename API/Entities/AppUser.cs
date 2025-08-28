
/// <summary>
/// Represents a user/member in the application.
/// Used by EF Core for database mapping and by API endpoints.
/// </summary>
namespace API.Entities;

public class AppUser
{
    // Unique identifier for user (GUID string)
    public string Id { get; set; } = Guid.NewGuid().ToString();
    // Display name shown in UI
    public required string DisplayName { get; set; }
    // User's email (used for login)
    public required string Email { get; set; }
    // Hashed password (never store plain text)
    public required byte[] PasswordHash { get; set; }
    // Salt used for hashing password
    public required byte[] PasswordSalt { get; set; }
}
