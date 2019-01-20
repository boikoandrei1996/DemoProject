using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Shared
{
  public class InternalServerErrorObjectResult : ObjectResult
  {
    public InternalServerErrorObjectResult(object value) : base(value)
    {
      StatusCode = StatusCodes.Status500InternalServerError;
    }
  }
}
