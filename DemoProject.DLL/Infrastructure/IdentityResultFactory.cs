using Microsoft.AspNetCore.Identity;

namespace DemoProject.DLL.Infrastructure
{
  public static class IdentityResultFactory
  {
    public static IdentityResult FailedResult(string code, string description)
    {
      var error = new IdentityError
      {
        Code = code,
        Description = description
      };

      return IdentityResult.Failed(error);
    }
  }
}
