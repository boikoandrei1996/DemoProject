using System;
using System.Threading.Tasks;
using DemoProject.DAL.Models;
using DemoProject.DAL.Models.Pages;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IOrderService : IChangeableService<Order>, IReadableService<Order, OrderPage>
  {
    Task<ServiceResult> ApproveAsync(Guid id);
    Task<ServiceResult> RejectAsync(Guid id);
    Task<ServiceResult> CloseAsync(Guid id);
  }
}
