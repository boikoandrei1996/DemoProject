using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Models;
using DemoProject.DLL.Models.Pages;

namespace DemoProject.DLL.Interfaces
{
  public interface IDiscountService : IService<Discount>
  {
    Task<List<Discount>> GetDiscountsAsync(Expression<Func<Discount, bool>> filter = null);
    Task<DiscountPage> GetPageDiscountsAsync(int pageIndex, int pageSize, Expression<Func<Discount, bool>> filter = null);
    Task<Discount> FindByAsync(Expression<Func<Discount, bool>> filter);
    Task<ChangeHistory> GetHistoryRecordAsync();
  }
}
