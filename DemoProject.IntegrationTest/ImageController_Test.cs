using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DemoProject.IntegrationTest.Infrastructure;
using Xunit;

namespace DemoProject.IntegrationTest
{
  public class ImageController_Test : TestFixture
  {
    [Fact]
    public async Task GetAll_Result_OK()
    {
      // Arrange

      // Act
      var response = await _client.GetAsync("/api/image/all");
      var results = await response.Content.ReadAsAsync<IEnumerable<string>>();

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.NotEmpty(results);
    }
  }
}
