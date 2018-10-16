using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;

namespace DemoProject.DLL.Interfaces
{
  public interface ICartService : IService<Cart>
  {
    Task<CartPage> GetPageAsync(int pageIndex, int pageSize, Expression<Func<Cart, bool>> filter = null);
    Task<List<Cart>> GetListAsync(Expression<Func<Cart, bool>> filter = null);
    Task<Cart> FindByAsync(Expression<Func<Cart, bool>> filter);
    Task<ServiceResult> AddItemToCartAsync(Guid cartId, Guid shopItemDetailId);
    Task<ServiceResult> RemoveItemFromCartAsync(Guid cartId, Guid shopItemDetailId, bool shouldBeRemovedAllItems);
  }
}