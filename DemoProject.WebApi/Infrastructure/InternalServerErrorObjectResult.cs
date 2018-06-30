using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.WebApi.Infrastructure
{
  public class InternalServerErrorObjectResult : ObjectResult
  {
    public InternalServerErrorObjectResult(object value) : base(value)
    {
      StatusCode = StatusCodes.Status500InternalServerError;
    }
  }
}
