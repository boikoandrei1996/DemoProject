using DemoProject.DAL.Configuration;
using DemoProject.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DemoProject.DAL
{
  public class EFContext : DbContext
  {
    public EFContext(DbContextOptions<EFContext> options) : base(options) { }

    public DbSet<AppUser> Users { get; set; }
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
      optionsBuilder.ConfigureWarnings(x => x.Throw(RelationalEventId.QueryClientEvaluationWarning));

      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder
        .ApplyConfiguration(new AppUserConfiguration())
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
      this.Users.DeleteFromQuery();
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
