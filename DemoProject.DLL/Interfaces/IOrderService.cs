using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;

namespace DemoProject.DLL.Interfaces
{
  public interface IOrderService : IService<Order>
  {
    Task<OrderPage> GetPageAsync(int pageIndex, int pageSize, Expression<Func<Order, bool>> filter = null);
    Task<List<Order>> GetListAsync(Expression<Func<Order, bool>> filter = null);
    Task<Order> FindByAsync(Expression<Func<Order, bool>> filter);
    Task<ServiceResult> ApproveAsync(Guid id);
    Task<ServiceResult> RejectAsync(Guid id);
    Task<ServiceResult> CloseAsync(Guid id);
  }
}
