using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DemoProject.DLL.Infrastructure
{
  public static class ServiceResultFactory
  {
    public static ServiceResult Success
    {
      get { return new ServiceResult(ServiceResultKey.Success); }
    }

    public static ServiceResult NotFound
    {
      get { return new ServiceResult(ServiceResultKey.NotFound); }
    }

    public static ServiceResult InternalServerErrorResult(string message)
    {
      return new ServiceResult(ServiceResultKey.InternalServerError, new ServiceError
      {
        Description = message
      });
    }

    public static ServiceResult BadRequestResult(string description)
    {
      return new ServiceResult(ServiceResultKey.BadRequest, new ServiceError
      {
        Description = description
      });
    }

    public static ServiceResult BadRequestResult(ModelStateDictionary modelState)
    {
      var result = new ServiceResult(ServiceResultKey.BadRequest);

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
