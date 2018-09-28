using System.Threading.Tasks;
using DemoProject.DLL.Models;

namespace DemoProject.DLL.Interfaces
{
  public interface ITrackableService
  {
    Task<ChangeHistory> GetHistoryRecordAsync(GroupName group);
  }
}
