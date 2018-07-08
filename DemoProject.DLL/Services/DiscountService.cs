using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Extensions;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Models;
using DemoProject.DLL.Pages;
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

    public Task<List<Discount>> GetDiscountsAsync(Expression<Func<Discount, bool>> filter = null)
    {
      var query = _context.Discounts.AsNoTracking();

      if (filter != null)
      {
        query = query.Where(filter);
      }

      return query.Include(x => x.Items).ToListAsync();
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

      page.Records = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(x => x.Items).ToListAsync();

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

      return await _context.SaveChangesSafeAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
