
/// <summary>
/// Exposes endpoints for retrieving user/member data.
/// Used by Angular frontend to display member lists and details.
/// </summary>
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // Inherits from BaseApiController for routing and API conventions
    public class MembersController(AppDbContext context) : BaseApiController
    {
        // GET: api/members
        // Returns all users (members) in the database
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync(); // EF Core async query
            return members;
        }

        // GET: api/members/{id}
        // Returns a single member by id. Requires JWT auth.
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.Users.FindAsync(id); // EF Core async find
            if (member == null) return NotFound();
            return member;
        }
    }
}
