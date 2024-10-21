using Microsoft.EntityFrameworkCore;
using Users.Core.Models;
using Users.DAL.Models.UsersModel;

namespace Users.DAL;

public class UsersDbContext : DbContext
{
  public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new UserConfiguration());
  }
}