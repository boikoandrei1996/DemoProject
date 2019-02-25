using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Shared.Extensions
{
  public static class DbContextExtensions
  {
    public static async Task<ServiceResult> SaveAsync(this DbContext context, string code, Guid modelId)
    {
      var result = await context.SaveAsync(code);

      return result.Key == ServiceResultKey.Success ?
        ServiceResultFactory.EntityCreatedResult(modelId) :
        result;
    }

    public static async Task<ServiceResult> SaveAsync(this DbContext context, string code, object model)
    {
      var result = await context.SaveAsync(code);

      return result.Key == ServiceResultKey.Success ?
        ServiceResultFactory.EntityUpdatedResult(model) :
        result;
    }

    public static async Task<ServiceResult> SaveAsync(this DbContext context, string code)
    {
      try
      {
        await context.SaveChangesAsync();
        return ServiceResultFactory.Success;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        return ServiceResultFactory.BadRequestResult(code, ex.InnerException.Message);
      }
      catch (DbUpdateException ex)
      {
        return ServiceResultFactory.BadRequestResult(code, ex.InnerException.Message);
      }
      catch (Exception ex)
      {
        return ServiceResultFactory.InternalServerErrorResult(ex.InnerException.Message);
      }
    }
  }
}
