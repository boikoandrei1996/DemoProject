using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;

namespace DemoProject.DLL.Interfaces
{
  public interface IContentGroupService : IChangeableService<ContentGroup>
  {
    Task<ChangeHistory> GetHistoryRecordAsync(GroupName group);
    Task<ContentGroupPage> GetPageAsync(GroupName group, int pageIndex, int pageSize, Expression<Func<ContentGroup, bool>> filter = null);
    Task<List<ContentGroup>> GetListAsync(GroupName group, Expression<Func<ContentGroup, bool>> filter = null);
    Task<ContentGroup> FindByAsync(GroupName group, Expression<Func<ContentGroup, bool>> filter);
  }
}
