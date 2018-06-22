using System;
using System.Threading.Tasks;
using DemoProject.DLL.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DLL.Extensions
{
  public static class ContextExtensions
  {
    public static Task<IdentityResult> SaveChangesSafeAsync(this DbContext context)
    {
      return ContextExtensions.SaveChangesSafeAsync(context, string.Empty);
    }

    public static async Task<IdentityResult> SaveChangesSafeAsync(this DbContext context, string code)
    {
      try
      {
        await context.SaveChangesAsync();
        return IdentityResult.Success;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        return IdentityResultFactory.FailedResult(code, ex.InnerException.Message);
      }
      catch (DbUpdateException ex)
      {
        return IdentityResultFactory.FailedResult(code, ex.InnerException.Message);
      }
      catch (Exception ex)
      {
        return IdentityResultFactory.FailedResult(code, ex.InnerException.Message);
      }
    }
  }
}
