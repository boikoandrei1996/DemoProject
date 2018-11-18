using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;
using DemoProject.Shared.Interfaces;

namespace DemoProject.DLL.Interfaces
{
  public interface IShopItemService : IChangeableService<ShopItem>, IReadableService<ShopItem, ShopItemPage>
  {
  }
}
