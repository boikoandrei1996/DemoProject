using System;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IShopItemService : IChangeableService<ShopItem, Guid>, IReadableService<ShopItem>, IDisposable
  {
  }
}
