using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Shared.Attributes
{
  public class EnumRangeValidationAttribute : ValidationAttribute
  {
    private readonly Type _enumType;

    public EnumRangeValidationAttribute(Type enumType)
    {
      _enumType = enumType;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (Enum.TryParse(_enumType, (string)value, true, out object temp))
      {
        return ValidationResult.Success;
      }
      else
      {
        var allowableValues = string.Join(", ", Enum.GetNames(_enumType));
        return new ValidationResult($"The {validationContext.DisplayName} field should be in range of [{allowableValues}].", new[] { validationContext.MemberName });
      }
    }
  }
}
