using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoProject.Shared.Interfaces
{
  public interface IReadableService<TModel>
  {
    Task<Page<TModel>> GetPageAsync(int pageIndex, int pageSize, Expression<Func<TModel, bool>> filter = null);
    Task<List<TModel>> GetListAsync(Expression<Func<TModel, bool>> filter = null);
    Task<TModel> FindByAsync(Expression<Func<TModel, bool>> filter);
  }
}
