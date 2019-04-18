using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Shared
{
  public sealed class InternalServerErrorResult : StatusCodeResult
  {
    public InternalServerErrorResult() : base(StatusCodes.Status500InternalServerError)
    {
    }
  }
}
