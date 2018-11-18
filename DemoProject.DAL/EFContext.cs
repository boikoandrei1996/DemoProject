using System;
using DemoProject.DAL.Configuration;
using DemoProject.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DAL
{
  public class EFContext : IdentityDbContext<AppUser, AppRole, Guid>
  {
    public EFContext(DbContextOptions<EFContext> options) : base(options) { }

    public DbSet<InfoObject> InfoObjects { get; set; }
    public DbSet<ContentGroup> ContentGroups { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<ShopItem> ShopItems { get; set; }
    public DbSet<ShopItemDetail> ShopItemDetails { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartShopItem> CartShopItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ChangeHistory> History { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder
        .ApplyConfiguration(new ShopItemConfiguration())
        .ApplyConfiguration(new ShopItemDetailConfiguration())
        .ApplyConfiguration(new CartShopItemConfiguration())
        .ApplyConfiguration(new MenuItemConfiguration())
        .ApplyConfiguration(new InfoObjectConfiguration())
        .ApplyConfiguration(new ContentGroupConfiguration())
        .ApplyConfiguration(new OrderConfiguration())
        .ApplyConfiguration(new ChangeHistoryConfiguration());
    }

    public void ClearDatabase()
    {
      this.Carts.DeleteFromQuery();
      this.CartShopItems.DeleteFromQuery();
      this.ContentGroups.DeleteFromQuery();
      this.InfoObjects.DeleteFromQuery();
      this.MenuItems.DeleteFromQuery();
      this.Orders.DeleteFromQuery();
      this.ShopItemDetails.DeleteFromQuery();
      this.ShopItems.DeleteFromQuery();
      this.History.DeleteFromQuery();
    }
  }
}
