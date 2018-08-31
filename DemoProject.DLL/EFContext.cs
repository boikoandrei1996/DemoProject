using System;
using DemoProject.DLL.Configuration;
using DemoProject.DLL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DLL
{
  public class EFContext : IdentityDbContext<AppUser, AppRole, Guid>
  {
    public EFContext(DbContextOptions<EFContext> options) : base(options)
    {
    }

    public DbSet<InfoObject> InfoObjects { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<ShopItem> ShopItems { get; set; }
    public DbSet<ShopItemDetail> ShopItemDetails { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartShopItem> CartShopItems { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.ApplyConfiguration(new ShopItemConfiguration());
      builder.ApplyConfiguration(new ShopItemDetailConfiguration());
      builder.ApplyConfiguration(new CartShopItemConfiguration());
      builder.ApplyConfiguration(new MenuItemConfiguration());
      builder.ApplyConfiguration(new InfoObjectConfiguration());
      builder.ApplyConfiguration(new DiscountConfiguration());
      builder.ApplyConfiguration(new OrderConfiguration());
    }
  }
}
