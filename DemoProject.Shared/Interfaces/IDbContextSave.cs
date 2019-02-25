using System;
using System.Threading.Tasks;

namespace DemoProject.Shared.Interfaces
{
  public interface IDbContextSave
  {
    Task<ServiceResult> SaveAsync(string code, Guid modelId);
    Task<ServiceResult> SaveAsync(string code, object model);
    Task<ServiceResult> SaveAsync(string code);
  }
}
