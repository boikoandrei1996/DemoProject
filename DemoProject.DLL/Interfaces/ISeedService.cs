using System.Threading.Tasks;

namespace DemoProject.DLL.Interfaces
{
  public interface ISeedService<TContext>
  {
    void SeedDatabase(TContext context);
    Task SeedDatabaseAsync(TContext context);
  }
}
