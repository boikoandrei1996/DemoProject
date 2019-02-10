using DemoProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DAL.Configuration
{
  class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
  {
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
      builder
        .HasIndex(x => x.Username)
        .IsUnique();

      builder.Property(x => x.Username)
        .IsRequired()
        .HasMaxLength(100);

      builder.Property(x => x.Role)
        .IsRequired()
        .HasMaxLength(20);

      builder.Property(x => x.PasswordHash)
        .IsRequired();

      builder.Property(x => x.PasswordSalt)
        .IsRequired();

      builder.Property(x => x.FirstName)
        .IsRequired()
        .HasMaxLength(100);

      builder.Property(x => x.LastName)
        .IsRequired()
        .HasMaxLength(100);

      builder
        .HasIndex(x => x.Email)
        .IsUnique();

      builder.Property(x => x.Email)
        .IsRequired()
        .HasMaxLength(100);

      builder.Property(x => x.EmailConfirmed)
        .IsRequired();

      builder.Property(x => x.PhoneNumber)
        .IsRequired()
        .HasMaxLength(20);
    }
  }
}
