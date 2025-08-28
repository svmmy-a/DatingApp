
/// <summary>
/// EF Core database context for the app.
/// Manages Users table and database connection.
/// Used by controllers and services for data access.
/// </summary>
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
       // Represents the Users table in the database
       public DbSet<AppUser> Users { get; set; }
}
