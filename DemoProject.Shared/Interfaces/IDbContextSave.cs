using System.Threading.Tasks;

namespace DemoProject.Shared.Interfaces
{
  public interface IDbContextSave
  {
    Task<ServiceResult> SaveAsync(string code);
  }
}
