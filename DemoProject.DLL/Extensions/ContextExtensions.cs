using System;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DLL.Extensions
{
  public static class ContextExtensions
  {
    public static void ClearDatabase(this EFContext context)
    {
      context.Carts.DeleteFromQuery();
      context.CartShopItems.DeleteFromQuery();
      context.ContentGroups.DeleteFromQuery();
      context.InfoObjects.DeleteFromQuery();
      context.MenuItems.DeleteFromQuery();
      context.Orders.DeleteFromQuery();
      context.ShopItemDetails.DeleteFromQuery();
      context.ShopItems.DeleteFromQuery();
      context.History.DeleteFromQuery();
    }

    /*public static Task<ServiceResult> SaveChangesSafeAsync(this DbContext context)
    {
      return context.SaveChangesSafeAsync(string.Empty);
    }

    public static Task<ServiceResult> SaveChangesSafeAsync(this DbContext context, Guid modelId)
    {
      return context.SaveChangesSafeAsync(string.Empty, modelId);
    }*/

    public static async Task<ServiceResult> TrySaveChangesAsync(this DbContext context, string code, Guid modelId)
    {
      var result = await context.TrySaveChangesAsync(code);

      return result.Key == ServiceResultKey.Success ?
        ServiceResultFactory.EntityCreatedResult(modelId) :
        result;
    }

    public static async Task<ServiceResult> TrySaveChangesAsync(this DbContext context, string code, object model)
    {
      var result = await context.TrySaveChangesAsync(code);

      return result.Key == ServiceResultKey.Success ?
        ServiceResultFactory.EntityUpdatedResult(model) :
        result;
    }

    public static async Task<ServiceResult> TrySaveChangesAsync(this DbContext context, string code)
    {
      try
      {
        await context.SaveChangesAsync();
        return ServiceResultFactory.Success;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        return ServiceResultFactory.BadRequestResult(string.Empty, ex.InnerException.Message);
      }
      catch (DbUpdateException ex)
      {
        return ServiceResultFactory.BadRequestResult(string.Empty, ex.InnerException.Message);
      }
      catch (Exception ex)
      {
        return ServiceResultFactory.InternalServerErrorResult(ex.InnerException.Message);
      }
    }
  }
}
