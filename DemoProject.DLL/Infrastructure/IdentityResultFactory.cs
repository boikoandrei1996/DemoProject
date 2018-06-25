using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

    public static IdentityResult FailedResult(ModelStateDictionary modelState)
    {
      var identityErrors = new List<IdentityError>(modelState.ErrorCount);

      foreach (var field in modelState)
      {
        foreach (var error in field.Value.Errors)
        {
          identityErrors.Add(new IdentityError
          {
            Code = field.Key,
            Description = error.ErrorMessage
          });
        }
      }

      return IdentityResult.Failed(identityErrors.ToArray());
    }
  }
}
