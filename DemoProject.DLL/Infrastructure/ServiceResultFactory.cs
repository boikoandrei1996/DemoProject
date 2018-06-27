using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DemoProject.DLL.Infrastructure
{
  public static class ServiceResultFactory
  {
    public static ServiceResult Success
    {
      get { return new ServiceResult(ServiceResultKey.Success); }
    }

    public static ServiceResult ServerErrorResult(string description)
    {
      return new ServiceResult(ServiceResultKey.ServerError, new ServiceError
      {
        Description = description
      });
    }

    public static ServiceResult DbErrorResult(string description)
    {
      return new ServiceResult(ServiceResultKey.DbError, new ServiceError
      {
        Description = description
      });
    }

    public static ServiceResult InvalidModelErrorResult(string description)
    {
      return new ServiceResult(ServiceResultKey.InvalidModelError, new ServiceError
      {
        Description = description
      });
    }

    public static ServiceResult InvalidModelErrorResult(ModelStateDictionary modelState)
    {
      var result = new ServiceResult(ServiceResultKey.ServerError);

      foreach (var field in modelState)
      {
        foreach (var error in field.Value.Errors)
        {
          result.Errors.Add(new ServiceError
          {
            Code = field.Key,
            Description = error.ErrorMessage
          });
        }
      }

      return result;
    }
  }
}
