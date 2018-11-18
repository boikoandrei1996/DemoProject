using System.Threading.Tasks;
using DemoProject.DAL.Models;
using DemoProject.DAL.Models.Pages;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IMenuItemService : IChangeableService<MenuItem>, IReadableService<MenuItem, MenuItemPage>
  {
    Task<ChangeHistory> GetHistoryRecordAsync();
  }
}
