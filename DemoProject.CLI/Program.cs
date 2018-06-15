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

      using (var context = new EFContext(optionsBuilder.Options))
      {
        context.InfoObjects.Add(new InfoObject
        {
          Content = "Test <b>info</b> object",
          Type = InfoObjectType.Text,
          Discount = new Discount { Title = "Test discount" }
        });
        context.SaveChanges();

        var obj = context.InfoObjects.Include(x => x.Discount).First();

        Console.WriteLine($"{obj.Discount.Title}({obj.Type.ToString()}): {obj.Content}");
      }

      Console.ReadLine();
    }
  }
}
