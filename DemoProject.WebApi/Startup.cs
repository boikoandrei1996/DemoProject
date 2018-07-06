using System.IO;
using DemoProject.DLL;
using DemoProject.DLL.Interfaces;
using DemoProject.DLL.Services;
using DemoProject.WebApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
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

      services.AddTransient<IDiscountService, DiscountService>();
      services.AddTransient<IInfoObjectService, InfoObjectService>();

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

        x.IncludeXmlComments(Path.Combine(Environment.ContentRootPath, @"bin\Debug\netcoreapp2.0\DemoProject.WebApi.xml"));
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

      app.ApplyMigrationAndDatabaseSeed();

      app.UseMvc();

      app.UseSwagger();
      app.UseSwaggerUI(x =>
      {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoProject API v1");
      });
    }
  }
}
