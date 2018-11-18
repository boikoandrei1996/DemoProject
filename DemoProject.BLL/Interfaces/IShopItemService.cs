using DemoProject.DAL.Models;
using DemoProject.DAL.Models.Pages;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IShopItemService : IChangeableService<ShopItem>, IReadableService<ShopItem, ShopItemPage>
  {
  }
}
