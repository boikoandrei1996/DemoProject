using System;
using DemoProject.DLL;
using DemoProject.DLL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DemoProject.WebApi.Infrastructure
{
  public static class ApplicationBuilderExtensions
  {
    public static void ApplyMigrationAndDatabaseSeed(this IApplicationBuilder app)
    {
      try
      {
        using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
          var context = scope.ServiceProvider.GetService<EFContext>();
          context.Database.Migrate();
          scope.ServiceProvider.GetService<ISeedService<EFContext>>().SeedDatabase(context);
        }
      }
      catch (Exception ex)
      {
        // TODO: Log here. "Failed to migrate or seed database."
      }
    }
  }
}
