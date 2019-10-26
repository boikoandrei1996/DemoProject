using System;
using System.IO;
using System.Text;
using DemoProject.BLL.Interfaces;
using DemoProject.BLL.Services;
using DemoProject.DAL;
using DemoProject.Shared;
using DemoProject.Shared.Interfaces;
using DemoProject.WebApi.Extensions;
using DemoProject.WebApi.Infrastructure;
using DemoProject.WebApi.Services;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace DemoProject.WebApi
{
  public class Startup
  {
    public static readonly bool Debug = true;

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
      var appSettings = appSettingsSection.Get<AppSettings>();
      services.Configure<AppSettings>(appSettingsSection);

      services.AddTransient<SeedService>(serviceProvider =>
      {
        return new SeedService(
          Path.Combine(Environment.WebRootPath, appSettings.SeedFilesFolderPath),
          serviceProvider.GetRequiredService<IPasswordManager>(),
          null);
      });

      services.AddTransient<AuthTokenGenerator>();
      services.AddTransient<ImageService>();
      services.AddTransient<IPasswordManager, PasswordManager>();
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
          options
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            .ConfigureWarnings(x => x.Throw(RelationalEventId.QueryClientEvaluationWarning));
        });

      services.AddCors();

      services
        .AddMvc(x => { })
        .AddJsonOptions(x =>
        {
          x.SerializerSettings.Formatting = Formatting.Indented;
          x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

      // configure jwt authentication
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

      services.AddElmah(options =>
      {
        // options.CheckPermissionAction = httpContext => httpContext.User.IsInRole(Role.Admin);
      });
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddFile(Configuration.GetSection("SerilogFile"));

      var appSettings = app.ApplicationServices.GetRequiredService<IOptions<AppSettings>>();
      var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();

      app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

      app.UseAuthentication();

      if (this.Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.ConfigureExceptionHandler(logger);
      }

      app.ConfigureStatusCodePages();

      app.UseElmah();

      // app.ApplyMigrationAndDatabaseSeed(appSettings.Value, logger);

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
