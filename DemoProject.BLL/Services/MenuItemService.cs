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
using DemoProject.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class MenuItemService : IMenuItemService
  {
    private readonly EFContext _context;

    public MenuItemService(EFContext context)
    {
      _context = context;
    }

    public Task<ChangeHistory> GetHistoryRecordAsync()
    {
      return _context.History
        .AsNoTracking()
        .OrderByDescending(x => x.TimeOfChange)
        .FirstOrDefaultAsync(x => x.Table == TableName.MenuItem);
    }

    public Task<Page<MenuItem>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<MenuItem, bool>> filter = null)
    {
      throw new NotImplementedException();
    }

    public Task<List<MenuItem>> GetListAsync(Expression<Func<MenuItem, bool>> filter = null)
    {
      var query = _context.MenuItems.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      return query.OrderBy(x => x.Order).ToListAsync();
    }

    public Task<MenuItem> FindByAsync(Expression<Func<MenuItem, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.MenuItems.AsNoTracking()
        .Include(x => x.Items)
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<MenuItem, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.MenuItems.AnyAsync(filter);
    }

    public Task<ServiceResult> AddAsync(MenuItem model)
    {
      Check.NotNull(model, nameof(model));

      _context.MenuItems.Add(model);
      _context.History.Add(ChangeHistory.Create(TableName.MenuItem, ActionType.Add));

      return _context.SaveAsync(nameof(AddAsync), model.Id);
    }

    public async Task<ServiceResult> UpdateAsync(MenuItem model)
    {
      Check.NotNull(model, nameof(model));

      if (await _context.MenuItems.AnyAsync(x => x.Id == model.Id) == false)
      {
        return ServiceResultFactory.NotFound;
      }
      
      _context.MenuItems.Update(model);
      _context.History.Add(ChangeHistory.Create(TableName.MenuItem, ActionType.Modify));

      return await _context.SaveAsync(nameof(UpdateAsync));
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await this.FindByAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }
      
      _context.MenuItems.Remove(model);
      _context.History.Add(ChangeHistory.Create(TableName.MenuItem, ActionType.Delete));

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
