using DemoProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoProject.DAL.Configuration
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
