using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using DemoProject.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class ShopItemService : IShopItemService
  {
    private readonly IDbContext _context;

    public ShopItemService(IDbContext context)
    {
      _context = context;
    }

    public async Task<Page<ShopItem>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<ShopItem, bool>> filter = null)
    {
      Check.Positive(pageIndex, nameof(pageIndex));
      Check.Positive(pageSize, nameof(pageSize));

      var query = _context.ShopItems.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var totalCount = await query.CountAsync();

      var page = Page<ShopItem>.Create(pageSize, pageIndex, totalCount);

      if (totalCount != 0)
      {
        page.Records = await query
          .OrderBy(x => x.Title)
          .Skip((page.Current - 1) * page.Size)
          .Take(page.Size)
          .Include(x => x.Details)
          .ToListAsync();

        foreach (var record in page.Records)
        {
          record.Details = record.Details.OrderBy(x => x.SubOrder).ToList();
        }
      }

      return page;
    }

    public async Task<List<ShopItem>> GetListAsync(Expression<Func<ShopItem, bool>> filter = null)
    {
      var query = _context.ShopItems.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var items = await query.Include(x => x.Details).OrderBy(x => x.Title).ToListAsync();
      foreach (var item in items)
      {
        item.Details = item.Details.OrderBy(x => x.SubOrder).ToList();
      }

      return items;
    }

    public Task<ShopItem> FindByAsync(Expression<Func<ShopItem, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.ShopItems.AsNoTracking()
        .Include(x => x.Details)
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<ShopItem, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.ShopItems.AnyAsync(filter);
    }

    public async Task<ServiceResult> AddAsync(ShopItem model)
    {
      Check.NotNull(model, nameof(model));

      var menuItemExist = await _context.MenuItems.AnyAsync(x => x.Id == model.MenuItemId);
      if (menuItemExist == false)
      {
        return ServiceResultFactory.BadRequestResult(nameof(model.MenuItemId), $"MenuItem not found with id: '{model.MenuItemId}'.");
      }

      _context.ShopItems.Add(model);

      return await _context.SaveAsync(nameof(AddAsync), model);
    }

    public Task<ServiceResult> UpdateAsync(ShopItem model)
    {
      throw new NotImplementedException();
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await this.FindByAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.ShopItems.Remove(model);

      return await _context.SaveAsync<ShopItem>(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
