using System.Threading.Tasks;

namespace DemoProject.Shared.Interfaces
{
  public interface IDbContextSave
  {
    Task<ServiceResult> SaveAsync<TModel>(string code, TModel model = null) where TModel : class;
  }
}
