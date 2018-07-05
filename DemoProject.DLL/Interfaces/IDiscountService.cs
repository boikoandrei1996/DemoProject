using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;

namespace DemoProject.DLL.Interfaces
{
  public interface IDiscountService : IDisposable
  {
    Task<bool> ExistDiscountAsync(Expression<Func<Discount, bool>> filter);
    Task<bool> ExistInfoObjectAsync(Expression<Func<InfoObject, bool>> filter);

    Task<List<Discount>> GetDiscountsAsync(Expression<Func<Discount, bool>> filter = null);
    Task<DiscountPage> GetPageDiscountsAsync(int pageIndex, int pageSize, Expression<Func<Discount, bool>> filter = null);
    Task<Discount> FindByAsync(Expression<Func<Discount, bool>> filter);

    Task<ServiceResult> AddAsync(Discount discount);
    Task<ServiceResult> UpdateAsync(Discount discount);
    Task<ServiceResult> DeleteAsync(Guid discountId);

    Task<ServiceResult> AddInfoObjectAsync(InfoObject infoObject);
    Task<ServiceResult> UpdateInfoObjectAsync(InfoObject infoObject);
    Task<ServiceResult> DeleteInfoObjectAsync(Guid infoObjectId);
  }
}
