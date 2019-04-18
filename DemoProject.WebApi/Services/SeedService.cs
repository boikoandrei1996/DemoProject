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
  public sealed class SeedService
  {
    private readonly string _dirPath;
    private readonly ILogger _logger;
    private readonly IPasswordManager _passwordManager;

    public SeedService(
      string dirPath,
      IPasswordManager passwordManager,
      ILogger logger)
    {
      _dirPath = dirPath;
      _passwordManager = passwordManager;
      _logger = logger;
    }

    public void SeedDatabase(IDbContext context)
    {
      this.SeedDatabaseAsync(context).Wait();
    }

    private async Task SeedDatabaseAsync(IDbContext context)
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

      var carts = SeedData.LoadCarts(menuItems.First().Items.First().Details);
      await this.SaveToDbAsync(context, carts);

      var orders = SeedData.LoadOrders(carts);
      await this.SaveToDbAsync(context, orders);
    }

    private async Task<T> LoadAsync<T>(string fileName)
    {
      try
      {
        var path = Path.Combine(_dirPath, fileName);

        var json = await File.ReadAllTextAsync(path, Encoding.UTF8);

        return JsonConvert.DeserializeObject<T>(json);
      }
      catch (Exception ex)
      {
        _logger.LogCritical(ex, nameof(LoadAsync));

        if (Startup.Debug)
        {
          throw ex;
        }
        else
        {
          return default(T);
        }
      }
    }

    private async Task SaveToDbAsync<T>(IDbContext context, IEnumerable<T> entities)
      where T : class
    {
      try
      {
        await context.Set<T>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException ex)
      {
        _logger.LogCritical(ex.InnerException, nameof(SaveToDbAsync));
        if (Startup.Debug)
        {
          throw ex;
        }
      }
      catch (DbUpdateException ex)
      {
        _logger.LogCritical(ex.InnerException, nameof(SaveToDbAsync));
        if (Startup.Debug)
        {
          throw ex;
        }
      }
      catch (Exception ex)
      {
        _logger.LogCritical(ex, nameof(SaveToDbAsync));
        if (Startup.Debug)
        {
          throw ex;
        }
      }
    }

    private void ManagePasswords(IEnumerable<AppUser> users)
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
