using System;
using System.Threading.Tasks;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IOrderService : IChangeableService<Order>, IReadableService<Order>
  {
    Task<ServiceResult> ApproveAsync(Guid id, Guid userId);
    Task<ServiceResult> RejectAsync(Guid id, Guid userId);
    Task<ServiceResult> CloseAsync(Guid id, Guid userId);
  }
}
