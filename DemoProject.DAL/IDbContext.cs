using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DAL
{
  public interface IDbContext : IDbContextBase, IDbContextSave, IDbContextClearable
  {
    DbSet<AppUser> Users { get; set; }
    DbSet<InfoObject> InfoObjects { get; set; }
    DbSet<ContentGroup> ContentGroups { get; set; }
    DbSet<MenuItem> MenuItems { get; set; }
    DbSet<ShopItem> ShopItems { get; set; }
    DbSet<ShopItemDetail> ShopItemDetails { get; set; }
    DbSet<Cart> Carts { get; set; }
    DbSet<CartShopItem> CartShopItems { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<ChangeHistory> History { get; set; }
  }
}
