using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Shared
{
  public sealed class InternalServerErrorObjectResult : ObjectResult
  {
    public InternalServerErrorObjectResult(object value) : base(value)
    {
      StatusCode = StatusCodes.Status500InternalServerError;
    }
  }
}
