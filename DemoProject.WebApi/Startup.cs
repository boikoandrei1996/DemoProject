using System.IO;
using DemoProject.DLL;
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
    public IHostingEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IHostingEnvironment environment)
    {
      Configuration = configuration;
      Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddDbContext<EFContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddTransient<SeedService>(serviceProvider =>
      {
        return new SeedService(Path.Combine(Environment.WebRootPath, "Files"));
      });

      services.AddMvc();
    }

    public void Configure(IApplicationBuilder app)
    {
      if (Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.ApplyMigrationAndDatabaseSeed();

      app.UseMvc();
    }
  }
}
