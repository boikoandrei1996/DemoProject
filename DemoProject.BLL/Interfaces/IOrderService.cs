using System;
using System.Threading.Tasks;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IOrderService : IChangeableService<Order, Guid>, IReadableService<Order>, IDisposable
  {
    Task<ServiceResult> ProccessOrderAsync(ProcessOrderType processOrder, Guid id, Guid userId);
  }
}
