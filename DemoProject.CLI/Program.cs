using System;
using System.IO;
using DemoProject.DLL;
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

      }

      Console.ReadLine();
    }
  }
}
