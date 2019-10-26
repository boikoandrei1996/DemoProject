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
  public class MenuItemService : BaseService<MenuItem>, IMenuItemService
  {
    public MenuItemService(IDbContext context) : base(context)
    {
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

    public async Task<ServiceResult> AddAsync(MenuItem model)
    {
      Check.NotNull(model, nameof(model));
      Check.NotNullOrEmpty(model.Text, nameof(model.Text));

      // Doesn't supported StringComparison.OrdinalIgnoreCase by SQL
      var menuItemExist = await this.ExistAsync(x => model.Text.ToLower() == x.Text.ToLower());
      if (menuItemExist)
      {
        return ServiceResultFactory.BadRequestResult(nameof(AddAsync), $"MenuItem text already exist.");
      }

      _context.MenuItems.Add(model);
      _context.History.Add(ChangeHistory.Create(TableName.MenuItem, ActionType.Add));

      var result = await _context.SaveAsync(nameof(AddAsync));
      result.SetModelIfSuccess(model);

      return result;
    }

    public async Task<ServiceResult> UpdateAsync(MenuItem model)
    {
      Check.NotNull(model, nameof(model));

      var menuItem = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == model.Id);
      if (menuItem == null)
      {
        return ServiceResultFactory.NotFound;
      }

      var changed = false;

      // update text
      if (Utility.IsModified(menuItem.Text, model.Text))
      {
        // Doesn't supported StringComparison.OrdinalIgnoreCase by SQL
        var menuItemTextExist = await _context.MenuItems.AnyAsync(x => model.Text.ToLower() == x.Text.ToLower());
        if (menuItemTextExist)
        {
          return ServiceResultFactory.BadRequestResult(nameof(UpdateAsync), $"MenuItem text is already exist.");
        }

        menuItem.Text = model.Text;
        changed = true;
      }

      // update order
      if (Utility.IsModified(menuItem.Order, model.Order))
      {
        menuItem.Order = model.Order;
        changed = true;
      }

      // update iconPath
      if (Utility.IsModified(menuItem.IconPath, model.IconPath))
      {
        menuItem.IconPath = model.IconPath;
        changed = true;
      }

      if (changed == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(UpdateAsync), "Nothing to update.");
      }

      _context.MenuItems.Update(menuItem);
      _context.History.Add(ChangeHistory.Create(TableName.MenuItem, ActionType.Modify));

      var result = await _context.SaveAsync(nameof(UpdateAsync));
      result.SetModelIfSuccess(menuItem);

      return result;
    }
  }
}
