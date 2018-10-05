using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using DemoProject.WebApi.Models.MenuItemApiModels;

namespace DemoProject.WebApi.Attributes
{
  public class ImageExistValidationAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var path = this.GetPathValue(validationContext.ObjectInstance);

      if (File.Exists(path))
      {
        return ValidationResult.Success;
      }
      else
      {
        return new ValidationResult($"File should exist on the server.");
      }
    }

    private string GetPathValue(object instance)
    {
      string filename = string.Empty;
      if (instance is MenuItemAddModel)
      {
        filename = (instance as MenuItemAddModel).IconFilename;
      }
      else if (instance is MenuItemEditModel)
      {
        filename = (instance as MenuItemEditModel).IconFilename;
      }
      else
      {
        throw new InvalidCastException(nameof(ImageExistValidationAttribute));
      }

      return Constants.DEFAULT_PATH_TO_IMAGE + filename;
    }
  }
}
