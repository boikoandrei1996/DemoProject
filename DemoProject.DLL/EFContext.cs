using System;
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

      // Could be moved to separate classes. See link: https://metanit.com/sharp/entityframeworkcore/2.13.php
      this.ShopItemInit(builder);
      this.ShopItemDetailInit(builder);
      this.CartShopItemInit(builder);
      this.MenuItemInit(builder);
      this.InfoObjectInit(builder);
      this.DiscountInit(builder);
      this.OrderInit(builder);
    }

    private void CartShopItemInit(ModelBuilder builder)
    {
      builder.Entity<CartShopItem>()
        .HasKey(x => new { x.CartId, x.ShopItemId });
    }

    private void ShopItemInit(ModelBuilder builder)
    {
      builder.Entity<ShopItem>()
        .HasIndex(x => x.Title)
        .IsUnique();

      builder.Entity<ShopItem>().Property(x => x.Title)
        .IsRequired()
        .HasMaxLength(255);

      builder.Entity<ShopItem>().Property(x => x.Image)
        .IsRequired();

      builder.Entity<ShopItem>().Property(x => x.ImageContentType)
        .IsRequired()
        .HasMaxLength(20);
    }

    private void ShopItemDetailInit(ModelBuilder builder)
    {
      builder.Entity<ShopItemDetail>().Property(x => x.Kind)
        .IsRequired()
        .HasMaxLength(100);

      builder.Entity<ShopItemDetail>().Property(x => x.Quantity)
        .IsRequired()
        .HasMaxLength(100);
    }

    private void MenuItemInit(ModelBuilder builder)
    {
      builder.Entity<MenuItem>()
        .HasIndex(x => x.Text)
        .IsUnique();

      builder.Entity<MenuItem>().Property(x => x.Text)
        .IsRequired()
        .HasMaxLength(100);

      builder.Entity<MenuItem>().Property(x => x.Icon)
        .IsRequired();

      builder.Entity<MenuItem>().Property(x => x.IconContentType)
        .IsRequired()
        .HasMaxLength(20);
    }

    private void InfoObjectInit(ModelBuilder builder)
    {
      builder.Entity<InfoObject>().Property(x => x.Content)
        .IsRequired();
    }

    private void DiscountInit(ModelBuilder builder)
    {
      builder.Entity<Discount>()
        .HasIndex(x => x.Title)
        .IsUnique();

      builder.Entity<Discount>().Property(x => x.Title)
        .IsRequired()
        .HasMaxLength(100);
    }

    private void OrderInit(ModelBuilder builder)
    {
      builder.Entity<Order>().Property(x => x.DateOfCreation)
        .HasDefaultValueSql("GETDATE()");

      builder.Entity<Order>().Property(x => x.Name)
        .IsRequired()
        .HasMaxLength(255);

      builder.Entity<Order>().Property(x => x.Mobile)
        .IsRequired()
        .HasMaxLength(20);

      builder.Entity<Order>().Property(x => x.Address)
        .IsRequired();
    }
  }
}
