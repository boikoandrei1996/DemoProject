using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DemoProject.DLL.Interfaces;

namespace DemoProject.DLL.Services
{
  public class SeedService : ISeedService<EFContext>
  {
    public void SeedDatabase(EFContext context)
    {
      this.SeedDatabaseAsync(context).Wait();
    }

    public Task SeedDatabaseAsync(EFContext context)
    {
      // throw new NotImplementedException();
      return Task.CompletedTask;
    }
  }
}
