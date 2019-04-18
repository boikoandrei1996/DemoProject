using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DemoProject.Shared.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class FileExistValidationAttribute : ValidationAttribute
  {
    private readonly string _rootPath;
    private readonly string _dirPath;

    public FileExistValidationAttribute(
      string webContentRootPath, 
      string fileDirectoryPath)
    {
      _rootPath = webContentRootPath;
      _dirPath = fileDirectoryPath;
    }

    public ValidationResult IsValid(string value)
    {
      return this.IsValidValue(value, string.Empty);
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      return this.IsValidValue((string)value, validationContext.MemberName);
    }

    private ValidationResult IsValidValue(string value, string memberName)
    {
      var path = Path.Combine(_rootPath, _dirPath, value);

      if (File.Exists(path))
      {
        return ValidationResult.Success;
      }
      else
      {
        return new ValidationResult("File should exist on the server.", new[] { memberName });
      }
    }
  }
}
