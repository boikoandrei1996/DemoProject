using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Extensions;
using DemoProject.BLL.Interfaces;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;
using DemoProject.Shared;
using Microsoft.EntityFrameworkCore;
using DemoProject.DLL;

namespace DemoProject.BLL.Services
{
  public class ContentGroupService : IContentGroupService
  {
    private readonly EFContext _context;

    public ContentGroupService(EFContext context)
    {
      _context = context;
    }

    public Task<ChangeHistory> GetHistoryRecordAsync(GroupName group)
    {
      var groupTableName = ChangeHistory.GetTableNameByGroupName(group);

      return _context.History
        .LastOrDefaultAsync(x => x.TableName == groupTableName || x.TableName == TableNames.InfoObject);
    }

    public async Task<ContentGroupPage> GetPageAsync(GroupName group, int pageIndex, int pageSize, Expression<Func<ContentGroup, bool>> filter = null)
    {
      var query = _context.ContentGroups.AsNoTracking().Where(x => x.GroupName == group);

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var totalCount = await query.CountAsync();

      var page = new ContentGroupPage
      {
        CurrentPage = pageIndex,
        PageSize = pageSize,
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
      };

      var records = await query
        .OrderBy(x => x.Order)
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize)
        .Include(x => x.Items)
        .ToListAsync();
      foreach (var record in records)
      {
        record.Items = record.Items.OrderBy(x => x.SubOrder).ToList();
      }

      page.Records = records;

      return page;
    }

    public async Task<List<ContentGroup>> GetListAsync(GroupName group, Expression<Func<ContentGroup, bool>> filter = null)
    {
      var query = _context.ContentGroups.AsNoTracking().Where(x => x.GroupName == group);

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var items = await query.Include(x => x.Items).OrderBy(x => x.Order).ToListAsync();
      foreach (var item in items)
      {
        item.Items = item.Items.OrderBy(x => x.SubOrder).ToList();
      }

      return items;
    }

    public Task<ContentGroup> FindByAsync(GroupName group, Expression<Func<ContentGroup, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.ContentGroups.AsNoTracking()
        .Include(x => x.Items)
        .Where(x => x.GroupName == group)
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<ContentGroup, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.ContentGroups.AnyAsync(filter);
    }

    public Task<ServiceResult> AddAsync(ContentGroup model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      var tableName = ChangeHistory.GetTableNameByGroupName(model.GroupName);
      _context.ContentGroups.Add(model);
      _context.History.Add(ChangeHistory.Create(tableName));

      return _context.SaveAsync(nameof(AddAsync), model.Id);
    }

    public async Task<ServiceResult> UpdateAsync(ContentGroup model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      if (await _context.ContentGroups.AnyAsync(x => x.Id == model.Id) == false)
      {
        return ServiceResultFactory.NotFound;
      }

      var tableName = ChangeHistory.GetTableNameByGroupName(model.GroupName);
      _context.ContentGroups.Update(model);
      _context.History.Add(ChangeHistory.Create(tableName));

      return await _context.SaveAsync(nameof(UpdateAsync));
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.ContentGroups.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      var tableName = ChangeHistory.GetTableNameByGroupName(model.GroupName);
      _context.ContentGroups.Remove(model);
      _context.History.Add(ChangeHistory.Create(tableName));

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
