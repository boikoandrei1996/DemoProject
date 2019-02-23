using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DemoProject.Shared.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class FileExistValidationAttribute : ValidationAttribute
  {
    public static readonly string CustomMessage = "File should exist on the server.";

    private readonly string _rootPath;
    private readonly string _dirPath;

    public FileExistValidationAttribute(
      string webContentRootPath, 
      string fileDirectoryPath)
    {
      _rootPath = webContentRootPath;
      _dirPath = fileDirectoryPath;
    }

    public bool IsNotValid(string value)
    {
      return this.IsValid(value) == false;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var path = Path.Combine(_rootPath, _dirPath, (string)value);

      if (File.Exists(path))
      {
        return ValidationResult.Success;
      }
      else
      {
        return new ValidationResult(
          FileExistValidationAttribute.CustomMessage,
          new[] { validationContext.MemberName });
      }
    }
  }
}
