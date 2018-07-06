using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;

namespace DemoProject.DLL.Interfaces
{
  public interface IService<T> : IDisposable
  {
    Task<bool> ExistAsync(Expression<Func<T, bool>> filter);
    Task<ServiceResult> AddAsync(T model);
    Task<ServiceResult> UpdateAsync(T model);
    Task<ServiceResult> DeleteAsync(Guid id);
  }
}
