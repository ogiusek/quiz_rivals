using Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Tests.Mocks.Entities;

internal class RelationlessEntityConfiguration : IEntityTypeConfiguration<RelationlessEntity>
{
  public void Configure(EntityTypeBuilder<RelationlessEntity> builder)
  {
    builder
      .ToTable("relationless_entities");


    builder
      .Property(x => x.Id)
      .HasColumnType("VARCHAR(36)")
      .HasConversion(x => x.Value, x => new Id(x))
      .IsRequired().ValueGeneratedNever();
    builder
      .HasKey(x => x.Id);


    builder
      .Property(x => x.Name)
      .HasColumnType("VARCHAR(64)")
      .HasConversion(x => x, x => x)
      .IsRequired();
    builder
      .HasIndex(x => x.Name)
      .IsUnique();


    builder
      .Property(x => x.Description)
      .HasColumnType("VARCHAR(64)")
      .HasConversion(x => x, x => x)
      .IsRequired(false);
  }
}