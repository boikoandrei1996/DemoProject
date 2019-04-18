using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoProject.Shared.Attributes
{
  [AttributeUsage(AttributeTargets.Class)]
  public sealed class ValidateModelStateAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      if (context.ModelState.IsValid)
      {
        return;
      }

      var errors = new List<ServiceError>();
      foreach (var field in context.ModelState)
      {
        foreach (var error in field.Value.Errors)
        {
          errors.Add(new ServiceError
          {
            Code = field.Key,
            Description = error.ErrorMessage
          });
        }
      }
      
      context.Result = new BadRequestObjectResult(errors);
    }
  }
}
