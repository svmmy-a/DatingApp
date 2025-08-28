/// <summary>
/// Handles user registration and login.
/// Uses EF Core for database, TokenService for JWT, and exposes endpoints for Angular frontend.
/// </summary>
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using API.Extensions;

namespace API.Controllers;

// Inherits from BaseApiController to get [ApiController] and [Route] attributes
public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
{
    // POST: api/account/register
    // Registers a new user. Called by Angular frontend.
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        // Check if email is already taken
        if (await EmailExists(registerDto.Email))
        {
            return BadRequest("Email is taken");
        }

        // Hash password with salt for security
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        // Add user to database
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Return user info + JWT token to frontend
        return user.ToDto(tokenService);
    }

    // POST: api/account/login
    // Authenticates user and returns JWT if successful
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        // Find user by email
        var user = await context.Users
            .SingleOrDefaultAsync(x => x.Email == loginDto.Email);

        if (user == null) return Unauthorized("Invalid email");

        // Hash input password with stored salt and compare
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }

        // Return user info + JWT token
        return user.ToDto(tokenService);
    }

    // Helper to check if email exists in DB
    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
    }
}
