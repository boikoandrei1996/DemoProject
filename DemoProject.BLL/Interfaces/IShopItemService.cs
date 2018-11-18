using DemoProject.BLL.PageModels;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IShopItemService : IChangeableService<ShopItem>, IReadableService<ShopItem, ShopItemPage>
  {
  }
}
