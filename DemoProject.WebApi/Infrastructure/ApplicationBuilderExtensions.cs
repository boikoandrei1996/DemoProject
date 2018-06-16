﻿using System;
using DemoProject.DLL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DemoProject.WebApi.Infrastructure
{
  public static class ApplicationBuilderExtensions
  {
    public static void ApplyMigrationAndDatabaseSeed(this IApplicationBuilder app)
    {
      using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        try
        {
          var context = scope.ServiceProvider.GetService<EFContext>();
          var databaseCreator = context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
          var isDatabaseExisted = databaseCreator.Exists();

          context.Database.Migrate();

          if (!isDatabaseExisted)
          {
            scope.ServiceProvider.GetService<SeedService>().SeedDatabase(context);
          }
        }
        catch (Exception ex)
        {
          var logger = scope.ServiceProvider.GetService<ILogger<Startup>>();
          if (logger != null && logger.IsEnabled(LogLevel.Error))
          {
            logger.LogError(ex, "Failed to migrate or seed database.");
          }
        }
      }
    }
  }
}
