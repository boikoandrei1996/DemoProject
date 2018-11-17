using System;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces.Shared;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;

namespace DemoProject.DLL.Interfaces
{
  public interface ICartService : IChangeableService<Cart>, IReadableService<Cart, CartPage>
  {
    Task<ServiceResult> AddItemToCartAsync(Guid cartId, Guid shopItemDetailId, int count);
    Task<ServiceResult> RemoveItemFromCartAsync(Guid cartId, Guid shopItemDetailId, bool shouldBeRemovedAllItems);
  }
}