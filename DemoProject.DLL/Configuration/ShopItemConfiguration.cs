using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DLL.Configuration
{
  class ShopItemConfiguration : IEntityTypeConfiguration<ShopItem>
  {
    public void Configure(EntityTypeBuilder<ShopItem> builder)
    {
      builder
        .HasIndex(x => x.Title)
        .IsUnique();

      builder.Property(x => x.Title)
        .IsRequired()
        .HasMaxLength(255);

      builder.Property(x => x.Image)
        .IsRequired()
        .HasColumnType("image");

      builder.Property(x => x.ImageContentType)
        .IsRequired()
        .HasMaxLength(20);
    }
  }
}
