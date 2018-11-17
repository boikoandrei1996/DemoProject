using DemoProject.DLL.Interfaces.Shared;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;

namespace DemoProject.DLL.Interfaces
{
  public interface IShopItemService : IChangeableService<ShopItem>, IReadableService<ShopItem, ShopItemPage>
  {
  }
}
