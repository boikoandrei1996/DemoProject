using DemoProject.DLL;
using DemoProject.DLL.Infrastructure;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Services;
using DemoProject.WebApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoProject.WebApi
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddDbContext<EFContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddTransient<IFileReadingService, JsonReadingService>(serviceProvider =>
      {
        return new JsonReadingService(@"C:\Users\BoikoAndrei1996\Documents\Visual Studio 2017\Projects\DemoProject\DemoProject.DLL\StaticContent\InitData");
      });

      services.AddTransient<ISeedService<EFContext>, SeedService>();

      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.ApplyMigrationAndDatabaseSeed();

      app.UseMvc();
    }
  }
}
