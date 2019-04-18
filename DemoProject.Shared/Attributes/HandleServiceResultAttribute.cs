using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoProject.Shared.Attributes
{
  /// <summary>
  /// Wrapper need for using DI for IHostingEnvironment in HandleServiceResultInnerAttribute.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public sealed class HandleServiceResultAttribute : TypeFilterAttribute
  {
    public HandleServiceResultAttribute() : base(typeof(HandleServiceResultInnerAttribute)) { }

    private sealed class HandleServiceResultInnerAttribute : ResultFilterAttribute
    {
      private readonly bool _isDevelopment;

      public HandleServiceResultInnerAttribute(
        IHostingEnvironment environment)
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
        Check.NotNull(result, nameof(result));

        switch (result.Key)
        {
          case ServiceResultKey.Success:
            if (result.Model == null)
            {
              return new OkResult();
            }
            else
            {
              return new OkObjectResult(result.Model);
            }
          case ServiceResultKey.BadRequest:
            return new BadRequestObjectResult(result.Errors);
          case ServiceResultKey.NotFound:
            return new NotFoundResult();
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
