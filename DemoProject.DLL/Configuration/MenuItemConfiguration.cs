using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DLL.Configuration
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

      builder.Property(x => x.IconContentType)
        .IsRequired()
        .HasMaxLength(20);
    }
  }
}
