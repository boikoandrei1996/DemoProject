using System.ComponentModel.DataAnnotations;

namespace DemoProject.Shared.Attributes
{
  public class MinimumValueValidationAttribute : ValidationAttribute
  {
    private readonly int _minValue;

    public MinimumValueValidationAttribute(int minValue = 1)
    {
      _minValue = minValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      return _minValue > (int)value ?
        new ValidationResult(
          $"The {validationContext.DisplayName} field should be more than {_minValue}.", 
          new[] { validationContext.MemberName }) :
        ValidationResult.Success;
    }
  }
}
