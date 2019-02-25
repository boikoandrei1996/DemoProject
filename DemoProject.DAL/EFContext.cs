using System;
using System.Threading.Tasks;
using DemoProject.DAL.Configuration;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DAL
{
  public class EFContext : DbContext, IDbContext
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

    public async Task<ServiceResult> SaveAsync(string code, Guid modelId)
    {
      var result = await this.SaveAsync(code);

      return result.Key == ServiceResultKey.Success ?
        ServiceResultFactory.EntityCreatedResult(modelId) :
        result;
    }

    public async Task<ServiceResult> SaveAsync(string code, object model)
    {
      var result = await this.SaveAsync(code);

      return result.Key == ServiceResultKey.Success ?
        ServiceResultFactory.EntityUpdatedResult(model) :
        result;
    }

    public async Task<ServiceResult> SaveAsync(string code)
    {
      try
      {
        await this.SaveChangesAsync();
        return ServiceResultFactory.Success;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        return ServiceResultFactory.BadRequestResult(code, ex.InnerException.Message);
      }
      catch (DbUpdateException ex)
      {
        return ServiceResultFactory.BadRequestResult(code, ex.InnerException.Message);
      }
      catch (Exception ex)
      {
        return ServiceResultFactory.InternalServerErrorResult(ex.InnerException.Message);
      }
    }
  }
}
