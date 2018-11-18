using System.Threading.Tasks;
using DemoProject.BLL.PageModels;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IMenuItemService : IChangeableService<MenuItem>, IReadableService<MenuItem>
  {
    Task<ChangeHistory> GetHistoryRecordAsync();
  }
}
