using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoProject.Shared.Attributes
{
  /// <summary>
  /// Need for using DI in HandleServiceResultInnerAttribute.
  /// </summary>
  public class HandleServiceResultAttribute : TypeFilterAttribute
  {
    public HandleServiceResultAttribute() : base(typeof(HandleServiceResultInnerAttribute)) { }

    private class HandleServiceResultInnerAttribute : ResultFilterAttribute
    {
      private readonly bool _isDevelopment;

      public HandleServiceResultInnerAttribute(IHostingEnvironment environment)
      {
        _isDevelopment = environment.IsDevelopment();
      }

      public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
      {
        if (context.Result is ObjectResult)
        {
          var result = context.Result as ObjectResult;

          if (result.DeclaredType == typeof(ServiceResult))
          {
            context.Result = this.BuildFromServiceResult(result.Value as ServiceResult);
          }
        }

        return base.OnResultExecutionAsync(context, next);
      }

      private IActionResult BuildFromServiceResult(ServiceResult result)
      {
        if (result == null)
        {
          throw new ArgumentNullException(nameof(result));
        }

        switch (result.Key)
        {
          case ServiceResultKey.Success:
            return new OkResult();
          case ServiceResultKey.ModelCreated:
            return new OkObjectResult(result.ModelId);
          case ServiceResultKey.ModelUpdated:
            return new OkObjectResult(result.Model);
          case ServiceResultKey.NotFound:
            return new NotFoundResult();
          case ServiceResultKey.BadRequest:
            return new BadRequestObjectResult(result.Errors);
          case ServiceResultKey.InternalServerError:
            if (_isDevelopment)
            {
              return new InternalServerErrorObjectResult(result.Errors);
            }
            else
            {
              return new InternalServerErrorResult();
            }
          default:
            throw new NotImplementedException(nameof(ServiceResultKey));
        }
      }
    }
  }
}
