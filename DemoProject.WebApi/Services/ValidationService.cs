using System.IO;
using DemoProject.Shared;
using DemoProject.Shared.Attributes;

namespace DemoProject.WebApi.Services
{
  public class ValidationService
  {
    public ServiceResult ValidateFileExist(string value, string memberName)
    {
      if (!File.Exists(Constants.GetRelativePathToImage(value)))
      {
        return ServiceResultFactory.BadRequestResult(memberName, FileExistValidationAttribute.CustomMessage);
      }

      return null;
    }
  }
}
