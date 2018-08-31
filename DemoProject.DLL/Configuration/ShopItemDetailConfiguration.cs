using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DLL.Configuration
{
  class ShopItemDetailConfiguration : IEntityTypeConfiguration<ShopItemDetail>
  {
    public void Configure(EntityTypeBuilder<ShopItemDetail> builder)
    {
      builder.Property(x => x.Kind)
        .IsRequired()
        .HasMaxLength(100);

      builder.Property(x => x.Quantity)
        .IsRequired()
        .HasMaxLength(100);
    }
  }
}
