using Newtonsoft.Json;

namespace DemoProject.WebApi.Infrastructure
{
  public sealed class ErrorDetails
  {
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Path { get; set; }

    public string ToJsonString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}
