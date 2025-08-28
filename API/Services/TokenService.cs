/// <summary>
/// Service for creating JWT tokens for authenticated users.
/// Used by AccountController to issue tokens on login/register.
/// </summary>
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using API.Entities;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    // Creates a JWT token for the given user
    public string CreateToken(AppUser user)
    {
        // Read secret key from config (appsettings.json)
        var tokenKey = config["TokenKey"] ?? throw new Exception("Token key not found");
        if (tokenKey.Length < 64) 
            throw new Exception("Your token key must be at least 64 characters long");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        // Claims are info about the user (used in API auth)
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id)
        };

        // Signing credentials (HMAC SHA512)
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Describe the token (claims, expiry, signing)
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        // Create and return the JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
