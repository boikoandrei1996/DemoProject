using System;
using System.IO;
using System.Linq;
using DemoProject.DLL;
using DemoProject.DLL.Models;
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
        context.Orders.Add(new Order
        {
          Name = "Test name",
          Address = "Test address",
          Mobile = "123456789",
          TotalPrice = 123.45m,
          Cart = new Cart()
        });
        context.SaveChanges();

        Console.WriteLine(DateTime.UtcNow);
        Console.WriteLine(context.Orders.Last().DateOfCreation);
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
