using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoProject.DLL;
using DemoProject.DLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DemoProject.WebApi.Services
{
  public class SeedService
  {
    private readonly string _dirPath;
    private readonly ILogger _logger;

    public SeedService(string dirPath) : this(dirPath, null) { }

    public SeedService(string dirPath, ILogger logger)
    {
      _dirPath = dirPath;
      _logger = logger;
    }

    public void SeedDatabase(EFContext context)
    {
      this.SeedDatabaseAsync(context).Wait();
    }

    public async Task SeedDatabaseAsync(EFContext context)
    {
      var menuItems = await this.LoadAsync<List<MenuItem>>("MenuItems.json");
      await this.SaveToDbAsync(context, menuItems);
      await context.History.AddAsync(ChangeHistory.Create(TableNames.MenuItem));

      var discounts = await this.LoadAsync<List<ContentGroup>>("Discounts.json");
      await this.SaveToDbAsync(context, discounts);
      await context.History.AddAsync(ChangeHistory.Create(TableNames.Discount));

      var delivery = await this.LoadAsync<List<ContentGroup>>("Delivery.json");
      await this.SaveToDbAsync(context, delivery);
      await context.History.AddAsync(ChangeHistory.Create(TableNames.Delivery));

      var aboutUs = await this.LoadAsync<List<ContentGroup>>("AboutUs.json");
      await this.SaveToDbAsync(context, aboutUs);
      await context.History.AddAsync(ChangeHistory.Create(TableNames.AboutUs));

      var carts = this.LoadCarts(menuItems.First().Items.First().Details);
      await this.SaveToDbAsync(context, carts);
    }

    private async Task<T> LoadAsync<T>(string fileName)
    {
      try
      {
        string path = Path.Combine(_dirPath, fileName);

        string json = await File.ReadAllTextAsync(path, Encoding.UTF8);

        return JsonConvert.DeserializeObject<T>(json);
      }
      catch (Exception ex)
      {
        if (_logger != null && _logger.IsEnabled(LogLevel.Error))
        {
          _logger.LogError(ex, $"{nameof(LoadAsync)} exception: type: \"{ex.GetType()}\" message: \"{ex.Message}\"");
        }

        return default(T);
      }
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

    private List<Cart> LoadCarts(IEnumerable<ShopItemDetail> shopItemDetails)
    {
      var carts = new List<Cart>();
      foreach (var shopItemDetail in shopItemDetails)
      {
        var cartShopItem = new CartShopItem
        {
          Count = 1,
          Price = shopItemDetail.Price,
          ShopItemDetailId = shopItemDetail.Id
        };
        carts.Add(new Cart { CartShopItems = new List<CartShopItem>() { cartShopItem } });
      }

      return carts;
    }
  }
}
