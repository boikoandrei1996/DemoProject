using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Models;

namespace DemoProject.DLL.Interfaces
{
  public interface ICartService : IService<Cart>
  {
    Task<Cart> FindByAsync(Expression<Func<Cart, bool>> filter);
    Task<ServiceResult> AddItemToCartAsync(Guid cartId, Guid shopItemDetailId);
    Task<ServiceResult> RemoveItemFromCartAsync(Guid cartId, Guid shopItemDetailId, bool shouldBeRemovedAllItems);
  }
}