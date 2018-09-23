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
  public class DiscountService : IDiscountService
  {
    private readonly EFContext _context;

    public DiscountService(EFContext context)
    {
      _context = context;
    }

    public async Task<List<Discount>> GetDiscountsAsync(Expression<Func<Discount, bool>> filter = null)
    {
      var query = _context.Discounts.AsNoTracking();

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

    public async Task<DiscountPage> GetPageDiscountsAsync(int pageIndex, int pageSize, Expression<Func<Discount, bool>> filter = null)
    {
      var query = _context.Discounts.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      var totalCount = await query.CountAsync();

      var page = new DiscountPage
      {
        CurrentPage = pageIndex,
        PageSize = pageSize,
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
      };

      var records = await query
        .OrderBy(x => x.Order)
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize).Include(x => x.Items)
        .ToListAsync();
      foreach (var record in records)
      {
        record.Items = record.Items.OrderBy(x => x.SubOrder).ToList();
      }

      page.Records = records;

      return page;
    }

    public Task<Discount> FindByAsync(Expression<Func<Discount, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.Discounts.AsNoTracking().Include(x => x.Items).FirstOrDefaultAsync(filter);
    }

    public Task<ChangeHistory> GetHistoryRecordAsync()
    {
      return _context.History
        .LastOrDefaultAsync(x => x.TableName == TableNames.Discounts || x.TableName == TableNames.InfoObjects);
    }

    public Task<bool> ExistAsync(Expression<Func<Discount, bool>> filter)
    {
      if (filter == null)
      {
        throw new ArgumentNullException(nameof(filter));
      }

      return _context.Discounts.AnyAsync(filter);
    }

    public Task<ServiceResult> AddAsync(Discount model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      _context.Discounts.Add(model);
      _context.History.Add(ChangeHistory.Create(TableNames.Discounts));

      return _context.SaveChangesSafeAsync(nameof(AddAsync), model.Id);
    }

    public async Task<ServiceResult> UpdateAsync(Discount model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      if (await _context.Discounts.AnyAsync(x => x.Id == model.Id) == false)
      {
        return ServiceResultFactory.NotFound;
      }

      _context.Discounts.Update(model);
      _context.History.Add(ChangeHistory.Create(TableNames.Discounts));

      return await _context.SaveChangesSafeAsync(nameof(UpdateAsync));
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var discount = await _context.Discounts.FirstOrDefaultAsync(x => x.Id == id);
      if (discount == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.Discounts.Remove(discount);
      _context.History.Add(ChangeHistory.Create(TableNames.Discounts));

      return await _context.SaveChangesSafeAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
