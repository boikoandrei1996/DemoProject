using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;
using DemoProject.WebApi.Models.InfoObjectApiModels;

namespace DemoProject.WebApi.Attributes
{
  public class EnumRangeValidationAttribute : ValidationAttribute
  {
    private string GetTypeValue(object instance)
    {
      string type = string.Empty;
      if (instance is InfoObjectEditModel)
      {
        type = (instance as InfoObjectEditModel).Type;
      }
      else if (instance is InfoObjectAddModel)
      {
        type = (instance as InfoObjectAddModel).Type;
      }
      else
      {
        throw new InvalidCastException(nameof(EnumRangeValidationAttribute));
      }

      return type;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var type = this.GetTypeValue(validationContext.ObjectInstance);

      if (Enum.TryParse(type, true, out InfoObjectType temp))
      {
        return ValidationResult.Success;
      }
      else
      {
        var allowableValues = String.Join(", ", Enum.GetNames(typeof(InfoObjectType)));
        return new ValidationResult($"The {nameof(Type)} field should be in range of [{allowableValues}].", new[] { nameof(Type) });
      }
    }
  }
}
