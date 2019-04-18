using System;
using System.Threading.Tasks;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface ICartService : IChangeableService<Cart, Guid>, IReadableService<Cart>, IDisposable
  {
    Task<ServiceResult> AddItemToCartAsync(Guid cartId, Guid shopItemDetailId, int count);
    Task<ServiceResult> RemoveItemFromCartAsync(Guid cartId, Guid shopItemDetailId, bool shouldBeRemovedAllItems);
  }
}