/// <summary>
/// Extension methods for AppUser entity.
/// Converts AppUser to UserDto (adds JWT token).
/// Used by AccountController to send user info to frontend.
/// </summary>
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

public static class AppUserExtensions
{
    // Converts AppUser to UserDto and generates JWT token
    public static UserDto ToDto(this AppUser user, ITokenService tokenservice)
    {
        return new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = tokenservice.CreateToken(user)
        };
    }
}