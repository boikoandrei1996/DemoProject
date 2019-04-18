using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Shared.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class MinimumValueValidationAttribute : ValidationAttribute
  {
    private readonly int _minValue;

    public MinimumValueValidationAttribute(int minValue = 1)
    {
      _minValue = minValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      return value is int && _minValue <= (int)value ?
        ValidationResult.Success :
        new ValidationResult(
          $"The {validationContext.DisplayName} field should be more than {_minValue}.",
          new[] { validationContext.MemberName });
    }
  }
}
