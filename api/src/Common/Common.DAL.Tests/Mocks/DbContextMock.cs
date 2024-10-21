using Common.DAL.Tests.Mocks.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.DAL.Tests.Mocks;

internal class DbContextMock : DbContext
{
  public DbContextMock(DbContextOptions<DbContextMock> options) : base(options) { }

  public DbSet<RelationlessEntity> RelationlessEntities { get; set; }
  public DbSet<OneToManyManyEntity> OneToManyEntities { get; set; }
  public DbSet<OneToManyOneEntity> OneToManyOtherEntities { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new RelationlessEntityConfiguration());
    modelBuilder.ApplyConfiguration(new OneToManyManyEntityConfiguration());
    modelBuilder.ApplyConfiguration(new OneToManyOneEntityConfiguration());
  }
}