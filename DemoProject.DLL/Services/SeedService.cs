using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DemoProject.DLL.Services
{
  public class SeedService : ISeedService<EFContext>
  {
    private readonly IFileReadingService _fileReader;
    private readonly ILogger _logger;

    public SeedService(IFileReadingService fileReader) : this(fileReader, null) { }

    public SeedService(IFileReadingService fileReader, ILogger logger)
    {
      _fileReader = fileReader;
      _logger = logger;
    }

    public void SeedDatabase(EFContext context)
    {
      this.SeedDatabaseAsync(context).Wait();
    }

    public async Task SeedDatabaseAsync(EFContext context)
    {
      var menuItems = await _fileReader.LoadAsync<List<MenuItem>>("MenuItems.json");
      await this.SaveToDbAsync(context, menuItems);

      var discounts = await _fileReader.LoadAsync<List<Discount>>("Discounts.json");
      await this.SaveToDbAsync(context, discounts);
    }

    private async Task SaveToDbAsync<T>(EFContext context, IEnumerable<T> entities)
      where T : class
    {
      try
      {
        await context.Set<T>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        if (_logger != null && _logger.IsEnabled(LogLevel.Error))
        {
          _logger.LogError(ex.InnerException, nameof(SaveToDbAsync));
        }
      }
      catch (DbUpdateException ex)
      {
        if (_logger != null && _logger.IsEnabled(LogLevel.Error))
        {
          _logger.LogError(ex.InnerException, nameof(SaveToDbAsync));
        }
      }
    }
  }
}
