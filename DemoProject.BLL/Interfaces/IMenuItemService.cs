using System;
using System.Threading.Tasks;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IMenuItemService : IChangeableService<MenuItem, Guid>, IReadableService<MenuItem>, IDisposable
  {
    Task<ChangeHistory> GetHistoryRecordAsync();
  }
}
