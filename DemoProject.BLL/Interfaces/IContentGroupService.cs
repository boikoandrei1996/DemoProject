using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.PageModels;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;

namespace DemoProject.BLL.Interfaces
{
  public interface IContentGroupService : IChangeableService<ContentGroup>
  {
    Task<ChangeHistory> GetHistoryRecordAsync(ContentGroupName group);
    Task<ContentGroupPage> GetPageAsync(ContentGroupName group, int pageIndex, int pageSize, Expression<Func<ContentGroup, bool>> filter = null);
    Task<List<ContentGroup>> GetListAsync(ContentGroupName group, Expression<Func<ContentGroup, bool>> filter = null);
    Task<ContentGroup> FindByAsync(ContentGroupName group, Expression<Func<ContentGroup, bool>> filter);
  }
}
