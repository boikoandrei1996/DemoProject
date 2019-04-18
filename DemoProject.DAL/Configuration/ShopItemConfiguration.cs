using DemoProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DAL.Configuration
{
  internal sealed class ShopItemConfiguration : IEntityTypeConfiguration<ShopItem>
  {
    public void Configure(EntityTypeBuilder<ShopItem> builder)
    {
      builder
        .HasIndex(x => x.Title)
        .IsUnique();

      builder.Property(x => x.Title)
        .IsRequired()
        .HasMaxLength(255);

      builder.Property(x => x.ImagePath)
        .IsRequired()
        .HasMaxLength(255);
    }
  }
}
