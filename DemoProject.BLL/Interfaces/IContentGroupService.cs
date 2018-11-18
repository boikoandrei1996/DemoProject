using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.PageModels;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IContentGroupService : IChangeableService<ContentGroup>
  {
    Task<ChangeHistory> GetHistoryRecordAsync(GroupName group);
    Task<ContentGroupPage> GetPageAsync(GroupName group, int pageIndex, int pageSize, Expression<Func<ContentGroup, bool>> filter = null);
    Task<List<ContentGroup>> GetListAsync(GroupName group, Expression<Func<ContentGroup, bool>> filter = null);
    Task<ContentGroup> FindByAsync(GroupName group, Expression<Func<ContentGroup, bool>> filter);
  }
}
