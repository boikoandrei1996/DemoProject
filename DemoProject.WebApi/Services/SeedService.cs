using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoProject.DAL;
using DemoProject.DAL.Enums;
using DemoProject.DAL.Models;
using DemoProject.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DemoProject.WebApi.Services
{
  public class SeedService
  {
    private readonly string _dirPath;
    private readonly ILogger _logger;
    private readonly IPasswordManager _passwordManager;

    public SeedService(string dirPath, IPasswordManager passwordManager) : this(dirPath, passwordManager, null) { }

    public SeedService(string dirPath, IPasswordManager passwordManager, ILogger logger)
    {
      _dirPath = dirPath;
      _logger = logger;
      _passwordManager = passwordManager;
    }

    public void SeedDatabase(EFContext context)
    {
      this.SeedDatabaseAsync(context).Wait();
    }

    public async Task SeedDatabaseAsync(EFContext context)
    {
      var users = await this.LoadAsync<List<AppUser>>("Users.json");
      this.ManagePasswords(users);
      await this.SaveToDbAsync(context, users);

      var menuItems = await this.LoadAsync<List<MenuItem>>("MenuItems.json");
      await context.History.AddAsync(ChangeHistory.Create(TableName.MenuItem, ActionType.Add));
      await this.SaveToDbAsync(context, menuItems);

      var discounts = await this.LoadAsync<List<ContentGroup>>("Discounts.json");
      await context.History.AddAsync(ChangeHistory.Create(TableName.Discount, ActionType.Add));
      await this.SaveToDbAsync(context, discounts);

      var delivery = await this.LoadAsync<List<ContentGroup>>("Delivery.json");
      await context.History.AddAsync(ChangeHistory.Create(TableName.Delivery, ActionType.Add));
      await this.SaveToDbAsync(context, delivery);

      var aboutUs = await this.LoadAsync<List<ContentGroup>>("AboutUs.json");
      await context.History.AddAsync(ChangeHistory.Create(TableName.AboutUs, ActionType.Add));
      await this.SaveToDbAsync(context, aboutUs);

      var carts = this.LoadCarts(menuItems.First().Items.First().Details);
      await this.SaveToDbAsync(context, carts);

      var orders = this.LoadOrders(carts);
      await this.SaveToDbAsync(context, orders);
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
        carts.Add(new Cart
        {
          CartShopItems = new List<CartShopItem>() { cartShopItem }
        });
      }

      return carts;
    }

    private List<Order> LoadOrders(IEnumerable<Cart> carts)
    {
      var orders = new List<Order>();
      var i = 0;
      foreach (var cart in carts)
      {
        i += 1;
        orders.Add(new Order
        {
          Name = $"Order #{i}",
          Mobile = "1234567",
          Address = $"Minsk pr.Pushkina {i}",
          CartId = cart.Id
        });
      }

      return orders;
    }

    private void ManagePasswords(List<AppUser> users)
    {
      foreach (var user in users)
      {
        var (hash, salt) = _passwordManager.CreatePasswordHash(user.Username);

        user.PasswordHash = hash;
        user.PasswordSalt = salt;
      }
    }
  }
}
