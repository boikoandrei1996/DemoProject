﻿using System;
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

    public Task<ServiceResult> AddAsync(Discount discount)
    {
      if (discount == null)
      {
        return Task.Run(() =>
          ServiceResultFactory.InvalidModelErrorResult("Discount reference is null."));
      }

      _context.Discounts.Add(discount);

      return _context.SaveChangesSafeAsync(nameof(AddAsync));
    }

    public async Task<ServiceResult> DeleteAsync(Guid discountId)
    {
      var discount = await _context.Discounts.FirstOrDefaultAsync(x => x.Id == discountId);
      if (discount == null)
      {
        return ServiceResultFactory.Success;
      }

      _context.Discounts.Remove(discount);

      return await _context.SaveChangesSafeAsync(nameof(DeleteAsync));
    }

    public Task<ServiceResult> AddInfoObjectAsync(InfoObject infoObject)
    {
      if (infoObject == null)
      {
        return Task.Run(() => 
          ServiceResultFactory.InvalidModelErrorResult("InfoObject reference is null."));
      }

      _context.InfoObjects.Add(infoObject);

      return _context.SaveChangesSafeAsync(nameof(AddInfoObjectAsync));
    }

    public async Task<ServiceResult> DeleteInfoObjectFromDiscountAsync(Guid discountId, Guid infoObjectId)
    {
      var infoObject = await _context.InfoObjects.FirstOrDefaultAsync(x => x.DiscountId == discountId && x.Id == infoObjectId);
      if (infoObject == null)
      {
        return ServiceResultFactory.Success;
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
