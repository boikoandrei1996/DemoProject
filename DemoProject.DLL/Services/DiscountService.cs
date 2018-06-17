using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Models;
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

    public Task<Discount> FindByAsync(Expression<Func<Discount, bool>> filter)
    {
      return _context.Discounts.AsNoTracking().Include(x => x.Items).FirstOrDefaultAsync(filter);
    }

    public async Task<IdentityResult> DeleteAsync(Guid id)
    {
      var discount = await _context.Discounts.FirstOrDefaultAsync(x => x.Id == id);
      if (discount == null)
      {
        return IdentityResult.Success;
      }

      _context.Discounts.Remove(discount);

      return await this.SaveChangesAsync(nameof(DeleteAsync));
    }

    public Task<IdentityResult> AddOrUpdateAsync(Discount discount)
    {
      if (discount == null)
      {
        return Task.Run(() => 
          IdentityResultFactory.FailedResult(nameof(this.AddOrUpdateAsync), "Discount reference is null."));
      }

      if (discount.Id == default(Guid))
      {
        return this.AddAsync(discount);
      }
      else
      {
        return this.UpdateAsync(discount);
      }
    }

    private Task<IdentityResult> AddAsync(Discount discount)
    {
      _context.Discounts.Add(discount);

      return this.SaveChangesAsync(nameof(AddAsync));
    }

    private Task<IdentityResult> UpdateAsync(Discount discount)
    {
      _context.Discounts.Update(discount);

      return this.SaveChangesAsync(nameof(UpdateAsync));
    }

    private async Task<IdentityResult> SaveChangesAsync(string code)
    {
      try
      {
        await _context.SaveChangesAsync();
        return IdentityResult.Success;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        return IdentityResultFactory.FailedResult(code, ex.Message);
      }
      catch (DbUpdateException ex)
      {
        return IdentityResultFactory.FailedResult(code, ex.Message);
      }
      catch (Exception ex)
      {
        return IdentityResultFactory.FailedResult(code, ex.Message);
      }
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
