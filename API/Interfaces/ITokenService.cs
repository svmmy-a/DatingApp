/// <summary>
/// Interface for JWT token creation service.
/// Allows for dependency injection and testing.
/// </summary>
using API.Entities;

namespace API.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}