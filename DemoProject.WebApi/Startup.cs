using System;
using System.IO;
using System.Text;
using DemoProject.BLL.Interfaces;
using DemoProject.BLL.Services;
using DemoProject.DAL;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;
using DemoProject.WebApi.Infrastructure;
using DemoProject.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
      var appSettingsSection = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      services.AddTransient<IPasswordManager, PasswordManager>();

      services.AddTransient<SeedService>(serviceProvider =>
      {
        return new SeedService(
          Path.Combine(Environment.WebRootPath, "Files"),
          serviceProvider.GetService<IPasswordManager>(),
          null);
      });

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
        .AddDbContext<EFContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddCors();

      services
        .AddMvc(x => { })
        .AddJsonOptions(x =>
        {
          x.SerializerSettings.Formatting = Formatting.Indented;
          x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

      // configure jwt authentication
      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);
      services
        .AddAuthentication(x =>
        {
          x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
          x.Events = new JwtBearerEvents
          {
            OnTokenValidated = async context =>
            {
              var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
              var userId = Guid.Parse(context.Principal.Identity.Name);

              var userExist = await userService.ExistAsync(user => user.Id == userId);
              if (userExist == false)
              {
                // return unauthorized if user no longer exists
                context.Fail("Unauthorized");
              }
            }
          };
          x.RequireHttpsMetadata = false;
          x.SaveToken = true;
          x.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
          };
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
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials());

      app.UseAuthentication();

      if (Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.ConfigureExceptionHandler();
      }

      app.ConfigureStatusCodePages();

      var appSettings = app.ApplicationServices.GetService<IOptions<AppSettings>>();

      app.ApplyMigrationAndDatabaseSeed(appSettings.Value.DatabaseRestore);

      app.UseMvc();
      app.UseFileServer();

      app.SetupDefaultPage("swagger/index.html");

      app.UseSwagger();
      app.UseSwaggerUI(x =>
      {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoProject API v1");
      });
    }
  }
}
