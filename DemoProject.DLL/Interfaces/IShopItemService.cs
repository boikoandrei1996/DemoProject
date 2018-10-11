using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;

namespace DemoProject.DLL.Interfaces
{
  public interface IShopItemService : IService<ShopItem>
  {
    Task<ShopItemPage> GetPageAsync(int pageIndex, int pageSize, Expression<Func<ShopItem, bool>> filter = null);
    Task<List<ShopItem>> GetListAsync(Expression<Func<ShopItem, bool>> filter = null);
    Task<ShopItem> FindByAsync(Expression<Func<ShopItem, bool>> filter);
  }
}
