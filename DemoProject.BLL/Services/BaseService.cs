using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DemoProject.DAL;
using DemoProject.Shared;
using DemoProject.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.BLL.Services
{
  public abstract class BaseService<TModel> : IDisposable
    where TModel : BaseEntity<Guid>
  {
    protected readonly IDbContext _context;

    public BaseService(IDbContext context)
    {
      _context = context;
    }

    public Task<bool> ExistAsync(Expression<Func<TModel, bool>> filter)
    {
      Check.NotNull(filter, nameof(filter));

      return _context.Set<TModel>().AnyAsync(filter);
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
      var model = await _context.Set<TModel>().FirstOrDefaultAsync(x => x.Id == id);
      if (model == null)
      {
        return ServiceResultFactory.NotFound;
      }

      _context.Set<TModel>().Remove(model);

      return await _context.SaveAsync(nameof(DeleteAsync));
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}
