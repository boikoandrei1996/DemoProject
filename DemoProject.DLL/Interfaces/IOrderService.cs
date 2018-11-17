using System;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces.Shared;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;

namespace DemoProject.DLL.Interfaces
{
  public interface IOrderService : IChangeableService<Order>, IReadableService<Order, OrderPage>
  {
    Task<ServiceResult> ApproveAsync(Guid id);
    Task<ServiceResult> RejectAsync(Guid id);
    Task<ServiceResult> CloseAsync(Guid id);
  }
}
