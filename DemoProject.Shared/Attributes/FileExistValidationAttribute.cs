using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DemoProject.Shared.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class FileExistValidationAttribute : ValidationAttribute
  {
    public static readonly string CustomMessage = "File should exist on the server.";

    private readonly string _relativePath;

    public FileExistValidationAttribute(string relativePath)
    {
      _relativePath = relativePath;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if (File.Exists(_relativePath + (string)value))
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
