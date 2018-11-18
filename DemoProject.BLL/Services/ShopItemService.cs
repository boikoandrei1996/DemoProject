using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.BLL.Interfaces;
using DemoProject.DAL;
using DemoProject.DAL.Extensions;
using DemoProject.DAL.Models;
using DemoProject.DAL.Models.Pages;
using DemoProject.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public class ShopItemService : IShopItemService
  {
    private readonly EFContext _context;

    public ShopItemService(EFContext context)
    {
      _context = context;
    }

    public async Task<ShopItemPage> GetPageAsync(int pageIndex, int pageSize, Expression<Func<ShopItem, bool>> filter = null)
    {
      var query = _context.ShopItems.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var totalCount = await query.CountAsync();

      var page = new ShopItemPage
      {
        CurrentPage = pageIndex,
        PageSize = pageSize,
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
      };

      var records = await query
        .OrderBy(x => x.Title)
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize)
        .Include(x => x.Details)
        .ToListAsync();
      foreach (var record in records)
      {
        record.Details = record.Details.OrderBy(x => x.SubOrder).ToList();
      }

      page.Records = records;

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
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.ShopItems.AsNoTracking()
        .Include(x => x.Details)
        .FirstOrDefaultAsync(filter);
    }

    public Task<bool> ExistAsync(Expression<Func<ShopItem, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.ShopItems.AnyAsync(filter);
    }

    public Task<ServiceResult> AddAsync(ShopItem model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      _context.ShopItems.Add(model);

      return _context.SaveAsync(nameof(AddAsync), model.Id);
    }

    public async Task<ServiceResult> UpdateAsync(ShopItem model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      if (await _context.ShopItems.AnyAsync(x => x.Id == model.Id) == false)
      {
        return ServiceResultFactory.NotFound;
      }

      _context.ShopItems.Update(model);

      return await _context.SaveAsync(nameof(UpdateAsync));
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.ShopItems.FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.ShopItems.Remove(model);

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
