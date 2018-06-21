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
using Microsoft.AspNetCore.Identity;
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
      return _context.Discounts.AsNoTracking().Include(x => x.Items).FirstOrDefaultAsync(filter);
    }

    public Task<IdentityResult> AddAsync(Discount discount)
    {
      if (discount == null)
      {
        return Task.Run(() =>
          IdentityResultFactory.FailedResult(nameof(this.AddAsync), "Discount reference is null."));
      }

      _context.Discounts.Add(discount);

      return _context.SaveChangesSafeAsync(nameof(AddAsync));
    }

    public async Task<IdentityResult> DeleteAsync(Guid discountId)
    {
      var discount = await _context.Discounts.FirstOrDefaultAsync(x => x.Id == discountId);
      if (discount == null)
      {
        return IdentityResult.Success;
      }

      _context.Discounts.Remove(discount);

      return await _context.SaveChangesSafeAsync(nameof(DeleteAsync));
    }

    public async Task<IdentityResult> AddInfoObjectAsync(Guid discountId, InfoObject infoObject)
    {
      if (infoObject == null)
      {
        return IdentityResultFactory.FailedResult(nameof(this.AddInfoObjectAsync), "InfoObject reference is null.");
      }

      var discount = await _context.Discounts.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == discountId);
      if (discount == null)
      {
        return IdentityResultFactory.FailedResult(nameof(this.AddInfoObjectAsync), $"Discount '{discountId}' is not found.");
      }

      discount.Items.Add(infoObject);

      return await _context.SaveChangesSafeAsync(nameof(AddInfoObjectAsync));
    }

    public async Task<IdentityResult> DeleteInfoObjectFromDiscountAsync(Guid discountId, Guid infoObjectId)
    {
      var infoObject = await _context.InfoObjects.FirstOrDefaultAsync(x => x.DiscountId == discountId && x.Id == infoObjectId);
      if (infoObject == null)
      {
        return IdentityResult.Success;
      }

      _context.InfoObjects.Remove(infoObject);

      return await _context.SaveChangesSafeAsync(nameof(DeleteInfoObjectFromDiscountAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
