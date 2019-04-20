using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class ContentGroupService : IContentGroupService
  {
    private readonly IDbContext _context;

    public ContentGroupService(IDbContext context)
    {
      _context = context;
    }

    public Task<ChangeHistory> GetHistoryRecordAsync(TableName table)
    {
      return _context.History
        .AsNoTracking()
        .OrderByDescending(x => x.TimeOfChange)
        .FirstOrDefaultAsync(x => x.Table == table || x.Table == TableName.InfoObject);
    }

    public async Task<Page<ContentGroup>> GetPageAsync(ContentGroupName group, int pageIndex, int pageSize, Expression<Func<ContentGroup, bool>> filter = null)
    {
      Check.Positive(pageIndex, nameof(pageIndex));
      Check.Positive(pageSize, nameof(pageSize));

      var query = _context.ContentGroups.AsNoTracking().Where(x => x.GroupName == group);

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var totalCount = await query.CountAsync();

      var page = Page<ContentGroup>.Create(pageSize, pageIndex, totalCount);

      if (totalCount != 0)
      {
        page.Records = await query
          .OrderBy(x => x.Order)
          .Skip((page.Current - 1) * page.Size)
          .Take(page.Size)
          .Include(x => x.Items)
          .ToListAsync();

        foreach (var record in page.Records)
        {
          record.Items = record.Items.OrderBy(x => x.SubOrder).ToList();
        }
      }

      return page;
    }

    public async Task<List<ContentGroup>> GetListAsync(ContentGroupName group, Expression<Func<ContentGroup, bool>> filter = null)
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

    public Task<ContentGroup> FindByAsync(ContentGroupName group, Expression<Func<ContentGroup, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.ContentGroups.AsNoTracking()
        .Include(x => x.Items)
        .Where(x => x.GroupName == group)
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<ContentGroup, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.ContentGroups.AnyAsync(filter);
    }

    public async Task<ServiceResult> AddAsync(ContentGroup model)
    {
      Check.NotNull(model, nameof(model));

      _context.ContentGroups.Add(model);
      _context.History.Add(ChangeHistory.Create(model.GroupName, ActionType.Add));

      var result = await _context.SaveAsync(nameof(AddAsync));
      result.SetModelIfSuccess(model);

      return result;
    }

    public async Task<ServiceResult> UpdateAsync(ContentGroup model)
    {
      Check.NotNull(model, nameof(model));

      if (await _context.ContentGroups.AnyAsync(x => x.Id == model.Id) == false)
      {
        return ServiceResultFactory.NotFound;
      }

      _context.ContentGroups.Update(model);
      _context.History.Add(ChangeHistory.Create(model.GroupName, ActionType.Modify));

      return await _context.SaveAsync(nameof(UpdateAsync));
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.ContentGroups.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.ContentGroups.Remove(model);
      _context.History.Add(ChangeHistory.Create(model.GroupName, ActionType.Delete));

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
