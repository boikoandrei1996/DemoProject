using System;
using System.IO;
using System.Linq;
using DemoProject.DLL;
using DemoProject.DLL.Models;
using DemoProject.DLL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DemoProject.CLI
{
  public class Program
  {
    public static IConfiguration Configuration { get; set; }

    public static void Main(string[] args)
    {
      using (var context = GetContext())
      {
        using (var service = new ContentGroupService(context))
        {
          var entities = service.GetListAsync(GroupName.Discount).GetAwaiter().GetResult();

          foreach (var entity in entities)
          {
            Console.WriteLine($"{entity.Id}. {entity.Title}");
            entity.Items.Select(x => x.Content).ToList().ForEach(x => Console.Write(x + ","));
            Console.WriteLine();
          }
        }
      }

      Console.ReadLine();
    }

    private static EFContext GetContext()
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

      Configuration = builder.Build();

      var connectionString = Configuration.GetConnectionString("DefaultConnection");
      if (string.IsNullOrEmpty(connectionString))
      {
        throw new ApplicationException("Not found configuration.");
      }

      var optionsBuilder = new DbContextOptionsBuilder<EFContext>();
      optionsBuilder.UseSqlServer(connectionString);

      return new EFContext(optionsBuilder.Options);
    }
  }
}
