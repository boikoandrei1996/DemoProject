using System;
using System.Net;
using DemoProject.DAL;
using DemoProject.WebApi.Infrastructure;
using DemoProject.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DemoProject.WebApi.Extensions
{
  public static class ApplicationBuilderExtensions
  {
    public static void ApplyMigrationAndDatabaseSeed(
      this IApplicationBuilder app,
      AppSettings appSettings,
      ILogger logger)
    {
      using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        try
        {
          var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
          var databaseCreator = context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
          var isDatabaseExisted = databaseCreator.Exists();

          context.Database.Migrate();

          if (isDatabaseExisted == false || appSettings.DatabaseRestore)
          {
            context.ClearDatabase();
            scope.ServiceProvider.GetRequiredService<SeedService>().SeedDatabase(context);
          }
        }
        catch (Exception ex)
        {
          logger.LogCritical(ex, "Failed to migrate or seed database.");
        }
      }
    }

    public static void SetupDefaultPage(this IApplicationBuilder app, string url)
    {
      var options = new RewriteOptions();
      options.AddRedirect("^$", url);
      app.UseRewriter(options);
    }

    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {
      app.UseExceptionHandler(appBuilder =>
      {
        appBuilder.Run(async httpContext =>
        {
          httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
          httpContext.Response.ContentType = "application/json";

          string path = string.Empty;
          var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
          if (exceptionHandlerFeature != null)
          {
            logger.LogError($"Path: {exceptionHandlerFeature.Path} | Error: {exceptionHandlerFeature.Error}");
            path = exceptionHandlerFeature.Path;
          }

          var errorDetails = new ErrorDetails
          {
            StatusCode = httpContext.Response.StatusCode,
            Message = "Internal Server Error.",
            Path = path
          };

          await httpContext.Response.WriteAsync(errorDetails.ToJsonString());
        });
      });
    }

    public static void ConfigureStatusCodePages(this IApplicationBuilder app)
    {
      app.UseStatusCodePages(appBuilder =>
      {
        appBuilder.Run(async httpContext =>
        {
          httpContext.Response.ContentType = "application/json";

          var statusCodeDetails = new StatusCodeDetails
          {
            StatusCode = httpContext.Response.StatusCode,
            Message = ReasonPhrases.GetReasonPhrase(httpContext.Response.StatusCode)
          };

          await httpContext.Response.WriteAsync(statusCodeDetails.ToJsonString());
        });
      });
    }
  }
}
