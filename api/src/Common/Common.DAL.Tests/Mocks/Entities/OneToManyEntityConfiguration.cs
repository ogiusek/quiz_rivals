using Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DAL.Tests.Mocks.Entities;

internal class OneToManyManyEntityConfiguration : IEntityTypeConfiguration<OneToManyManyEntity>
{
  public void Configure(EntityTypeBuilder<OneToManyManyEntity> builder)
  {
    builder
      .ToTable("one_to_many_entities");


    builder
      .Property(x => x.Id)
      .HasColumnType("VARCHAR(36)")
      .HasConversion(x => x.Value, x => new Id(x))
      .IsRequired().ValueGeneratedNever();
    builder
      .HasKey(x => x.Id);


    builder
      .Property(x => x.OtherEntityId)
      .HasColumnType("VARCHAR(36)")
      .HasConversion(x => x.Value, x => new Id(x))
      .IsRequired();


    builder
      .HasOne(x => x.OtherEntity)
      .WithMany(x => x.Entities)
      .HasForeignKey(x => x.OtherEntityId)
      .HasPrincipalKey(x => x.Id);
  }
}

internal class OneToManyOneEntityConfiguration : IEntityTypeConfiguration<OneToManyOneEntity>
{
  public void Configure(EntityTypeBuilder<OneToManyOneEntity> builder)
  {
    builder
      .ToTable("one_to_many_other_entities");

    builder
      .Property(x => x.Id)
      .HasColumnType("VARCHAR(36)")
      .HasConversion(x => x.Value, x => new Id(x))
      .IsRequired().ValueGeneratedNever();
    builder
      .HasKey(x => x.Id);


    builder
      .HasMany(x => x.Entities)
      .WithOne(x => x.OtherEntity)
      .HasForeignKey(x => x.OtherEntityId)
      .HasPrincipalKey(x => x.Id);

  }
}