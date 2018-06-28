using System;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DLL.Extensions
{
  public static class ContextExtensions
  {
    public static Task<ServiceResult> SaveChangesSafeAsync(this DbContext context)
    {
      return ContextExtensions.SaveChangesSafeAsync(context, string.Empty);
    }

    public static async Task<ServiceResult> SaveChangesSafeAsync(this DbContext context, string code)
    {
      try
      {
        await context.SaveChangesAsync();
        return ServiceResultFactory.Success;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        return ServiceResultFactory.BadRequestResult(ex.InnerException.Message);
      }
      catch (DbUpdateException ex)
      {
        return ServiceResultFactory.BadRequestResult(ex.InnerException.Message);
      }
      catch (Exception ex)
      {
        return ServiceResultFactory.InternalServerErrorResult(ex.InnerException.Message);
      }
    }
  }
}
