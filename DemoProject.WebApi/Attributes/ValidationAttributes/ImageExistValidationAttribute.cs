using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DemoProject.WebApi.Attributes.ValidationAttributes
{
  public class ImageExistValidationAttribute : ValidationAttribute
  {
    private readonly string _path;

    public ImageExistValidationAttribute(string path)
    {
      _path = path;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (File.Exists(_path + (string)value))
      {
        return ValidationResult.Success;
      }
      else
      {
        return new ValidationResult($"File should exist on the server.", new[] { validationContext.MemberName });
      }
    }
  }
}
