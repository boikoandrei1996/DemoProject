using DemoProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DAL.Configuration
{
  class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
  {
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
      builder
        .HasIndex(x => x.Text)
        .IsUnique();

      builder.Property(x => x.Text)
        .IsRequired()
        .HasMaxLength(100);

      builder.Property(x => x.IconPath)
        .IsRequired()
        .HasMaxLength(255);
    }
  }
}
