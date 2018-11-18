using System;
using System.Threading.Tasks;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;

namespace DemoProject.DLL.Interfaces
{
  public interface IOrderService : IChangeableService<Order>, IReadableService<Order, OrderPage>
  {
    Task<ServiceResult> ApproveAsync(Guid id);
    Task<ServiceResult> RejectAsync(Guid id);
    Task<ServiceResult> CloseAsync(Guid id);
  }
}
