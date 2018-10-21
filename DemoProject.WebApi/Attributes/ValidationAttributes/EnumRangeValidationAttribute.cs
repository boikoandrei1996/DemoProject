using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.DLL.Models;

namespace DemoProject.WebApi.Attributes.ValidationAttributes
{
  public class EnumRangeValidationAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (Enum.TryParse((string)value, true, out InfoObjectType temp))
      {
        return ValidationResult.Success;
      }
      else
      {
        var allowableValues = string.Join(", ", Enum.GetNames(typeof(InfoObjectType)));
        return new ValidationResult($"The {validationContext.DisplayName} field should be in range of [{allowableValues}].", new[] { validationContext.MemberName });
      }
    }
  }
}
