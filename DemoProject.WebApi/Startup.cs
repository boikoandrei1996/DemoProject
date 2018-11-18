using System.IO;
using DemoProject.BLL.Interfaces;
using DemoProject.BLL.Services;
using DemoProject.DLL;
using DemoProject.WebApi.Infrastructure;
using DemoProject.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace DemoProject.WebApi
{
  public class Startup
  {
    public IConfiguration Configuration { get; }
    public IHostingEnvironment Environment { get; }

    public Startup(
      IConfiguration configuration,
      IHostingEnvironment environment)
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

      services.AddTransient<IImageService>(serviceProvider =>
      {
        return new ImageService(Environment.WebRootPath);
      });

      services.AddTransient<IContentGroupService, ContentGroupService>();
      services.AddTransient<IInfoObjectService, InfoObjectService>();
      services.AddTransient<IMenuItemService, MenuItemService>();
      services.AddTransient<IShopItemService, ShopItemService>();
      services.AddTransient<IShopItemDetailService, ShopItemDetailService>();
      services.AddTransient<ICartService, CartService>();
      services.AddTransient<IOrderService, OrderService>();

      services
        .AddMvc(x =>
        {
        })
        .AddJsonOptions(x =>
        {
          x.SerializerSettings.Formatting = Formatting.Indented;
          x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

      services.AddSwaggerGen(x =>
      {
        x.SwaggerDoc("v1", new Info
        {
          Version = "v1",
          Title = "DemoProject API",
          Description = "Pet's project",
          Contact = new Contact
          {
            Name = "Andrei Boika",
            Email = "BoikoAndrei1996@gmail.com"
          }
        });

        // x.IncludeXmlComments(Path.Combine(Environment.ContentRootPath, @"bin\Debug\netcoreapp2.0\DemoProject.WebApi.xml"));
        x.DescribeAllEnumsAsStrings();
      });

      // services.AddCors();
    }

    public void Configure(IApplicationBuilder app)
    {
      // app.UseCors(builder => builder.AllowAnyOrigin());

      if (Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.SetupDefaultPage("swagger/index.html");

      var isDatabaseRestore = Configuration.GetValue("DatabaseConfig:ShouldBeRestored", false);
      app.ApplyMigrationAndDatabaseSeed(isDatabaseRestore);

      app.UseMvc();
      app.UseFileServer();

      app.UseSwagger();
      app.UseSwaggerUI(x =>
      {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoProject API v1");
      });
    }
  }
}
