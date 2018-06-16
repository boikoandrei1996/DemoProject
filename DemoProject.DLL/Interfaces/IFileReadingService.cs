using System.Threading.Tasks;

namespace DemoProject.DLL.Interfaces
{
  public interface IFileReadingService
  {
    Task<T> LoadAsync<T>(string fileName);
  }
}
