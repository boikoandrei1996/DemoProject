using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DLL.Configuration
{
  class CartShopItemConfiguration : IEntityTypeConfiguration<CartShopItem>
  {
    public void Configure(EntityTypeBuilder<CartShopItem> builder)
    {
      builder
        .HasKey(x => new { x.CartId, x.ShopItemDetailId });
    }
  }
}
