using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Extensions;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DLL.Services
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
        .LastOrDefaultAsync(x => x.TableName == TableNames.MenuItem);
    }

    public Task<MenuItemPage> GetPageAsync(int pageIndex, int pageSize, Expression<Func<MenuItem, bool>> filter = null)
    {
      throw new NotImplementedException();
    }

    public async Task<List<MenuItem>> GetListAsync(Expression<Func<MenuItem, bool>> filter = null)
    {
      var query = _context.MenuItems.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      return await query.OrderBy(x => x.Order).ToListAsync();
    }

    public Task<MenuItem> FindByAsync(Expression<Func<MenuItem, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.MenuItems.AsNoTracking()
        .Include(x => x.Items)
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<MenuItem, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.MenuItems.AnyAsync(filter);
    }

    public Task<ServiceResult> AddAsync(MenuItem model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }
      
      _context.MenuItems.Add(model);
      _context.History.Add(ChangeHistory.Create(TableNames.MenuItem));

      return _context.SaveAsync(nameof(AddAsync), model.Id);
    }

    public async Task<ServiceResult> UpdateAsync(MenuItem model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      if (await _context.MenuItems.AnyAsync(x => x.Id == model.Id) == false)
      {
        return ServiceResultFactory.NotFound;
      }
      
      _context.MenuItems.Update(model);
      _context.History.Add(ChangeHistory.Create(TableNames.MenuItem));

      return await _context.SaveAsync(nameof(UpdateAsync));
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }
      
      _context.MenuItems.Remove(model);
      _context.History.Add(ChangeHistory.Create(TableNames.MenuItem));

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
