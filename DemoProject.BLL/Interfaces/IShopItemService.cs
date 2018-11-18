using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IShopItemService : IChangeableService<ShopItem>, IReadableService<ShopItem, ShopItemPage>
  {
  }
}
