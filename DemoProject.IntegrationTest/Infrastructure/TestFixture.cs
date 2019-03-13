using System;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace DemoProject.IntegrationTest.Infrastructure
{
  public class TestFixture : IDisposable
  {
    protected readonly TestServer _server;
    protected readonly HttpClient _client;

    public TestFixture()
    {
      var webHostBuilder = WebHost
        .CreateDefaultBuilder()
        .UseStartup<FakeStartup>()
        .UseWebRoot(@"C:\Users\BoikoAndrei\Documents\Visual Studio 2017\Projects\DemoProject\DemoProject.WebApi\wwwroot")
        .UseEnvironment("Development");

      _server = new TestServer(webHostBuilder);
      _client = _server.CreateClient();
    }


    public void Dispose()
    {
      _client.Dispose();
      _server.Dispose();
    }
  }
}
