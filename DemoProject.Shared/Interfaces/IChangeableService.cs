using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DemoProject.Shared.Interfaces
{
  public interface IChangeableService<T> : IDisposable
  {
    Task<bool> ExistAsync(Expression<Func<T, bool>> filter);
    Task<ServiceResult> AddAsync(T model);
    Task<ServiceResult> UpdateAsync(T model);
    Task<ServiceResult> DeleteAsync(Guid id);
  }
}
