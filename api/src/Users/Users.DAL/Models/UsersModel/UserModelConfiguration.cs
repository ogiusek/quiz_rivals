using Common.ValueObjects;
using FileSaver.Adapter.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Core.Models;
using Users.Core.Models.UserModel;

namespace Users.DAL.Models.UsersModel;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder
      .ToTable("users");


    builder
      .Property(x => x.Id)
      .HasColumnName("id")
      .HasColumnType("VARCHAR(36)")
      .HasConversion(x => x.Value, x => new Id(x))
      .IsRequired().ValueGeneratedNever();
    builder
      .HasKey(x => x.Id);


    builder
      .Property(x => x.Nick)
      .HasColumnName("nick")
      .HasColumnType("VARCHAR(64)")
      .HasConversion(x => x.Value, x => new UserNick(x))
      .IsRequired();
    builder
      .HasIndex(x => x.Nick)
      .IsUnique();


    builder
      .Property(x => x.PhotoPath)
      .HasColumnName("photo_path")
      .HasColumnType("VARCHAR(64)")
      .HasConversion(x => x.Path, x => new FilePath(x))
      .IsRequired();


    builder
      .Property(x => x.Email)
      .HasColumnName("email")
      .HasColumnType("VARCHAR(64)")
      .HasConversion(x => x.Value, x => new Email(x))
      .IsRequired(false);


    builder
      .Property(x => x.PasswordHash)
      .HasColumnName("password_hash")
      .HasColumnType("VARCHAR(64)")
      .HasConversion(x => x.Value, x => new Hash(x))
      .IsRequired(false);


    builder
      .Property(x => x.CreatedAt)
      .HasColumnName("created_at")
      .HasColumnType("timestamp without time zone")
      .HasConversion(x => DateTime.SpecifyKind(x, DateTimeKind.Unspecified), x => DateTime.SpecifyKind(x, DateTimeKind.Utc))
      .IsRequired().ValueGeneratedNever();
  }
}