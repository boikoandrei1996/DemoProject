using DemoProject.BLL.Interfaces;
using DemoProject.BLL.Services;
using DemoProject.DAL;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;
using DemoProject.WebApi.Infrastructure;
using DemoProject.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DemoProject.IntegrationTest.Infrastructure
{
  public class FakeStartup
  {
    public IConfiguration Configuration { get; }
    public IHostingEnvironment Environment { get; }

    public FakeStartup(
      IConfiguration configuration,
      IHostingEnvironment environment)
    {
      Configuration = configuration;
      Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddTransient<IPasswordManager, PasswordManager>();
      services.AddTransient<ImageService>();
      services.AddTransient<AuthTokenGenerator>();
      services.AddTransient<IUserService, UserService>();
      services.AddTransient<IContentGroupService, ContentGroupService>();
      services.AddTransient<IInfoObjectService, InfoObjectService>();
      services.AddTransient<IMenuItemService, MenuItemService>();
      services.AddTransient<IShopItemService, ShopItemService>();
      services.AddTransient<IShopItemDetailService, ShopItemDetailService>();
      services.AddTransient<ICartService, CartService>();
      services.AddTransient<IOrderService, OrderService>();

      services
        .AddDbContext<IDbContext, EFContext>(options =>
        {
          options.UseInMemoryDatabase("DemoProjectTestDb");
        });

      services.AddCors();

      services
        .AddMvc(x => { })
        .AddJsonOptions(x =>
        {
          x.SerializerSettings.Formatting = Formatting.Indented;
          x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials());

      app.UseMvc();
      app.UseFileServer();
    }
  }
}
