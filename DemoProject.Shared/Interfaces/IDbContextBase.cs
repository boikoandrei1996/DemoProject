using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DemoProject.Shared.Interfaces
{
  public interface IDbContextBase : IDisposable
  {
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
  }
}
