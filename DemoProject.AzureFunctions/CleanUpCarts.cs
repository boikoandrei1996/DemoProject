using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DemoProject.DAL;
using DemoProject.DAL.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DemoProject.AzureFunctions
{
  // TODO: Should be tested
  public static class CleanUpCarts
  {
    private static readonly string LocalConString = "Server=(localdb)\\mssqllocaldb;Database=DemoProjectDB.Web;Trusted_Connection=True;MultipleActiveResultSets=true";

    [FunctionName("CleanUpCarts")]
    public static async Task RunAsync(
      [TimerTrigger("0 */1 * * * *")]TimerInfo myTimer,
      CancellationToken token,
      ILogger log)
    {
      log.LogInformation($"CleanUpCarts timer function started at: {DateTime.Now}");

      using (var context = CleanUpCarts.CreateContext())
      {
        var cartsForRemoving = CleanUpCarts.GetCartsForRemoving(context);
        var success = await CleanUpCarts.TryRemoveCarts(context, cartsForRemoving, token, log);

        if (success)
        {
          log.LogInformation($"Carts exist: {context.Carts.Count()}");
          log.LogInformation($"CleanUpCarts timer function finished at: {DateTime.Now}");
        }
      }
    }

    public static EFContext CreateContext()
    {
      // var connectionString = Environment.GetEnvironmentVariable("sdb_connection");
      var optionsBuilder = new DbContextOptionsBuilder<EFContext>();
      optionsBuilder
        .UseSqlServer(CleanUpCarts.LocalConString)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

      return new EFContext(optionsBuilder.Options);
    }

    public static ICollection<Cart> GetCartsForRemoving(EFContext context)
    {
      var carts = context.Carts.ToList();

      // skip carts which are created less than 1 day ago
      var now = DateTime.UtcNow;
      carts.RemoveAll(x => x.DateOfCreation > now.AddDays(-1));

      // skip carts which are already in order
      foreach (var order in context.Orders)
      {
        carts.RemoveAll(x => x.Id == order.CartId);
      }

      return carts;
    }

    public static async Task<bool> TryRemoveCarts(
      EFContext context,
      ICollection<Cart> cartsForRemoving,
      CancellationToken token,
      ILogger log)
    {
      try
      {
        context.Carts.RemoveRange(cartsForRemoving);
        await context.SaveChangesAsync(token);
        return true;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        log.LogError(ex, ex.Message);
      }
      catch (DbUpdateException ex)
      {
        log.LogError(ex, ex.Message);
      }
      catch (Exception ex)
      {
        log.LogError(ex, ex.Message);
      }

      return false;
    }
  }
}
